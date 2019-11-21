import React, { useState } from "react";
import { Button, FormGroup, FormControl, FormLabel, Container, Row, Col as Column, Form} from "react-bootstrap";
import Paper from "@material-ui/core/Paper";
import { FacebookLoginButton } from "react-social-login-buttons";

export default function Login(props) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function validateForm() {
    return email.length > 0 && password.length > 0;
  }

  function handleSubmit(event) {
    event.preventDefault();
  }

  return (
    <Container>
      <Row className="justify-content-center">
        <Column xs='10' s='10' md='10' lg='4' xl='4'>
          <Paper elevation={10}>
            <Form className="p-5">
              <FormGroup controlId="email">
                <FormLabel>Email</FormLabel>
                <FormControl
                  autoFocus
                  type="email"
                  value={email}
                  onChange={e => setEmail(e.target.value)}
                />
              </FormGroup>
              <FormGroup controlId="password">
                <FormLabel>Password</FormLabel>
                <FormControl
                  value={password}
                  onChange={e => setPassword(e.target.value)}
                  type="password"
                />
              </FormGroup>
              <Button disabled={!validateForm()} type="submit">
                Login
              </Button>
            </Form>
          </Paper>
        </Column>
      </Row>
      <Row className="justify-content-center mt-2">
        <Column xs='10' s='10' md='10' lg='4' xl='4'>
          <FacebookLoginButton align="center" style={{width: '100%', margin: '0px'}}>
            Prisijungti su Facebook
          </FacebookLoginButton>
        </Column>
      </Row>
    </Container>
    // <div className="Login">
    //   <form onSubmit={handleSubmit}>
    //     <FormGroup controlId="email" bsSize="large">
    //       <FormLabel>Email</FormLabel>
    //       <FormControl
    //         autoFocus
    //         type="email"
    //         value={email}
    //         onChange={e => setEmail(e.target.value)}
    //       />
    //     </FormGroup>
    //     <FormGroup controlId="password" bsSize="large">
    //       <FormLabel>Password</FormLabel>
    //       <FormControl
    //         value={password}
    //         onChange={e => setPassword(e.target.value)}
    //         type="password"
    //       />
    //     </FormGroup>
    //     <Button block bsSize="large" disabled={!validateForm()} type="submit">
    //       Login
    //     </Button>
    //   </form>
    // </div>
  );
}