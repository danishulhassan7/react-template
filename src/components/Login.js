import React from 'react';


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Login = () => {
  return (
    <div>
    <TopBar/>
    <Nav/>
      <section class="section-bottom-border login-signup">
  <div class="container">
    <div class="row">
      <div class="login-main template-form">
        <h4>Please Log In, or <a href="#">Sign Up</a></h4>
        <div class="template-space"></div>
        <div class="row">
          <div class="col-xs-6 col-sm-6 col-md-6"> <a href="#" class="btn btn-facebook btn-block">Facebook</a> </div>
          <div class="col-xs-6 col-sm-6 col-md-6"> <a href="#" class="btn btn-google btn-block">Google</a> </div>
        </div>
        <div class="login-or">
          <hr class="hr-or"/>
          <span class="span-or">or</span> </div>
        <form>
          <div class="form-group">
            <label for="inputUsernameEmail" >Username or email</label>
            <input type="text" class="form-control" id="inputUsernameEmail"></input>
          </div>
          <div class="form-group"> <a class="pull-right" href="#">Forgot password?</a>
            <label for="inputPassword">Password</label>
            <input type="password" class="form-control" id="inputPassword"></input>
          </div>
          <div class="checkbox pull-right">
            
              <input type="checkbox"></input>
              <label> Remember me </label>
          </div>
          <button type="submit" class="btn btn btn-primary"> Log In </button>
        </form>
      </div>
    </div>
  </div>
</section>
<KonnectSlider/>
<Footer/>
    </div>
  );
};

export default Login;
