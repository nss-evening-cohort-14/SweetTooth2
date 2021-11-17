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
  setUserAddresses
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
            user={user}
            order={order}
            orderItems={orderItems}
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
        />}
        user={user}
        />
        <PrivateRoute
        exact path="/admin-dashboard"
        component={AdminDashboard}
        user={user}
        />
        <PrivateRoute
        exact path='/processed'
        component={Processed}
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
  userAddresses: PropTypes.array,
  setUserAddresses: PropTypes.func,
  setOrder: PropTypes.func,
  setOrderItems: PropTypes.func
};

export default Routes;
