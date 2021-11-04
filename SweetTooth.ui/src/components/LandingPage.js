import React from 'react';
// import PropTypes from 'prop-types';
import {
  Button, Container
} from 'reactstrap';
import styled from 'styled-components';
import logo from '../Assets/SweetToothLogo.png';

const LandingPageContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 50%;
  // border: 1px solid gray;
  padding: 10px;
  margin: 5px auto;
  // border-radius: 5px;
  // background-color: rgb(16,24,30);
`;

const LandingPageLogo = styled.img`
  background-image: url(${logo}) no-repeat center center fixed;
  display: inline-block;
  width: 50%;
  border-radius: 50%;
  padding 20x;
`;

function LandingPage() {
  return (
    <LandingPageContainer>
      <Container
        className="bg-light border"
        fluid="md"
      >
        <h1>Welcome to SweetTooth!</h1>
        <LandingPageLogo src={logo} alt="Logo"/>
        <h3>Already have an Account?</h3>
          <Button>
            Sign In
          </Button>
          <h3>New to SweetTooth?  You are in for a treat!</h3>
          <Button>
            Create an Account
          </Button>
      </Container>
    </LandingPageContainer>
  );
}

// LandingPage.propTypes = {

// };

export default LandingPage;
