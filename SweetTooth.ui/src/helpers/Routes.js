import React from 'react';
import {
  Switch,
  Route,
  Redirect
} from 'react-router-dom';
import PropTypes from 'prop-types';
import AdminDashboard from '../components/AdminDashboard';
import Cart from '../components/Cart';
import LandingPage from '../components/LandingPage';
import ShoppingPage from '../components/ShoppingPage';
import UserProfile from '../components/UserProfile';
import Processed from '../components/Processed';

// will need to add private routes once auth is done.
// Admin and user hook

const PrivateRoute = ({ component: Component, user, ...rest }) => {
  const routeChecker = (remainder) => (user
    ? (<Component {...remainder} user={user} />)
    : (<Redirect to={{ pathname: '/', state: { from: remainder.location } }} />));

  return <Route {...rest} render={(props) => routeChecker(props)} />;
};

PrivateRoute.propTypes = {
  component: PropTypes.func,
  user: PropTypes.any
};
function Routes({
  user,
  order,
  setOrder,
  orderItems,
  setOrderItems,
  userAddresses,
  setUserAddresses,
  snacks,
  setSnacks,
  paymentMethodsArray,
  setPaymentMethodsArray
}) {
  return (
    <div>
      <Switch>
        <Route
          exact path="/"
          component={() => <LandingPage
            user={user}
          />}
        />
        <PrivateRoute
          exact path="/shop"
          component={() => <ShoppingPage
            order={order}
            setOrder={setOrder}
            snacks={snacks}
          />}
          user={user}
        />
        <PrivateRoute
          exact path="/cart"
          component={() => <Cart
            user={user}
            order={order}
            orderItems={orderItems}
            setOrder={setOrder}
            setOrderItems={setOrderItems}
        />
      }
        user={user}
        />
        <PrivateRoute
          exact path="/user-profile"
          component={() => <UserProfile
            user={user}
            userAddresses={userAddresses}
            setUserAddresses={setUserAddresses}
            paymentMethodsArray={paymentMethodsArray}
            setPaymentMethodsArray={setPaymentMethodsArray}
          />}
          user={user}
        />
        <PrivateRoute
          exact path="/admin-dashboard"
          component={() => <AdminDashboard
            user={user}
            snacks={snacks}
            setSnacks={setSnacks}
          />}
          user={user}
        />
        <PrivateRoute
        exact path='/processed'
        component={() => <Processed
        firstName={user.firstName}
        number={order.orderNumber}
        />
        }
        user={user}
        />
      </Switch>
    </div>
  );
}

Routes.propTypes = {
  user: PropTypes.any,
  order: PropTypes.object,
  orderItems: PropTypes.array,
  setOrderItems: PropTypes.func,
  userAddresses: PropTypes.array,
  setUserAddresses: PropTypes.func,
  setOrder: PropTypes.func,
  snacks: PropTypes.array,
  setSnacks: PropTypes.func,
  paymentMethodsArray: PropTypes.array,
  setPaymentMethodsArray: PropTypes.func
};

export default Routes;
