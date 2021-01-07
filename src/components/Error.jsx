import React from "react"


import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Error=()=>{

    return(
        <>
        <TopBar/>
        <Nav/>
        <section class="section-bottom-border">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 error-page text-center">
      <img style={{width:"150"}} alt="logo" src={require('../img/icons/404.png')} />
        
        <h3><i class="fa fa-chain-broken" aria-hidden="true"></i> Page Not Found, Are you looking for below mentioned pages</h3>
        <ul>
          <li><Link to="/Home/About">About US</Link></li>
          <li><Link to="/Home/Contact">Contact Us</Link></li>
          <li><Link to="/Home/Course">Courses</Link></li>
          <li><Link to="/Home/Faq">Faq</Link></li>
        </ul>
      </div>
    </div>
  </div>
</section>
<KonnectSlider/>
<Footer/>
        </>
    )
}

export default Error;