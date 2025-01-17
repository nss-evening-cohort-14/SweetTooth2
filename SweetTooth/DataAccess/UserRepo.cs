﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SweetTooth.Models;
using Dapper;

namespace SweetTooth.DataAccess
{
    public class UserRepo
    {
        readonly string _connectionString;

        public UserRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SweetTooth");
        }

        internal User GetById(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);

            var userSql = @"Select * 
                            From [User]
                            Where Id = @id";

            var user = db.QuerySingleOrDefault<User>(userSql, new { id = userId });

            if (user == null) return null;

            var addressSql = @"Select *
                                From UserAddress ua
                                Where ua.UserId = @userId";

            var address = db.Query<UserAddress>(addressSql, new { userId });

            user.Addresses = address;
            return user;
        }

        internal IEnumerable<User> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var users = db.Query<User>(@"Select * From [User]");

            return users;
        }

        internal void Add(User newUser)
        {
            using var db = new SqlConnection(_connectionString);

            var userSql = @"Insert into [User] 
                                (FirebaseId,
                                [Admin], 
                                FirstName, 
                                LastName,
                                Email,
                                ProfileUrl,
                                DateCreated,
                                MoodId,
                                SoftDelete)
                            Output inserted.Id
                            Values 
                                (@FirebaseId,
                                @Admin,
                                @FirstName,
                                @LastName,
                                @Email, 
                                @ProfileUrl,
                                @DateCreated,
                                @MoodId,
                                @SoftDelete)";

            newUser.DateCreated = DateTime.Now;

            var userId = db.ExecuteScalar<Guid>(userSql, newUser);
            newUser.Id = userId;
        }

        internal object UpdateUser(Guid id, User user)
        {
            using var db = new SqlConnection(_connectionString);

            var updateUserSql = @"Update [User]
                        Set Admin = @Admin,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            MoodId = @MoodId,
                            SoftDelete = @SoftDelete
                        Output Inserted.*
                        Where id = @id; ";

            user.Id = id;
            var updatedUser = db.QuerySingleOrDefault<User>(updateUserSql, user);

            return updatedUser;
        }

        internal object SoftDelete(Guid id, User user)
        {
            using var db = new SqlConnection(_connectionString);

            var updateUserSql = @"Update [User]
                        Set Admin = @Admin,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            MoodId = @MoodId,
                            SoftDelete = @SoftDelete
                        Output Inserted.*
                        Where id = @id; ";

            user.Id = id;
            var updatedUser = db.QuerySingleOrDefault<User>(updateUserSql, user);

            return updatedUser;

        }

        internal User GetUserByUid(string firebaseId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"Select *
                        from [User] u
                        where u.FirebaseId = @firebaseId";

            var addressSql = @"Select *
                                from UserAddress
                                Where UserId = @id";

            var paymentSql = @"Select *
                               from PaymentMethod
                               Where UserId = @id";

            var user = db.QuerySingleOrDefault<User>(sql, new { firebaseId });

            var addresses = db.Query<UserAddress>(addressSql, new { id = user.Id });

            var payment = db.Query<PaymentMethod>(paymentSql, new { id = user.Id });
            user.Addresses = addresses;
            user.PaymentMethods = payment;

            return user;
        }
    }
}
