import React from "react"

import { Link } from 'react-router-dom';
import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Contact=()=>{
    return(<>
      <TopBar/>
      <Nav/>
        <section class="contact-page">
        <div class="container">
          <div class="row"> 
            <div class="col-sm-6 template-form">
              <h2 class="contact-heading">Get in <span>Touch with us</span></h2>
              <div class="template-space"></div>
              <form  id="contactForm">
                <input type="text" class="form-control" id="name" placeholder="Your Name" required></input>
                <input type="text" class="form-control" id="phone" placeholder="Your Phone number" required></input>
                <input type="email" class="form-control" id="email" placeholder="Enter Your E-Mail" required></input>
                <textarea class="form-control" id="comment" placeholder="Comment here..." required></textarea>
                <button type="submit" class="service-box-button">Submit</button>
              </form>
            </div>
            <div class="col-sm-6 small-map">
              <div id="map"></div>
              <div id="cont" style={{display:"none"}}>
              <img style={{width:"120"}} alt="logo" src={require('../img/logo-green.png')} />
        
                <p style={{color: "#333",fontSize: "16px",fontWeight: "400;"}}>Koramangala, Banglore<br/>
                  Karnatka, IND</p>
              </div>
            </div>
          </div>
        </div>
      </section>
      <aside class="light-bg contact-address">
        <div class="container">
          <div class="row"> 
            <div class="col-md-4 contact-box">

            <img alt="icon" class="template-contact-icon" src={require('../img/icons/location.png')} />
        
              <p>#Koramangala, Banglore<br/>
                Karnataka, INDIA</p>
            </div>
            
            <div class="col-md-4 contact-box"> 
            <img alt="icon" class="template-contact-icon" src={require('../img/icons/phone.png')} />
        
              <p>+91 123-456789<br/>
                +91 123-456780</p>
            </div>
            
            <div class="col-md-4 contact-box"> 
            <img alt="icon" class="template-contact-icon" src={require('../img/icons/email.png')} />
        
              <p>info@konnectplugins.com<br/>
                sales@konnectplugins.com</p>
            </div>
          </div>
        </div>
      </aside>
      
      
      <aside class="dark-bg aside-cta">
        <div class="container text-center">
          <div class="row">
            <div class="col-sm-12 col-xs-12">
              <h3 class="margin-10 text-white cta-heading">Are you Looking for good Trainer??</h3>
              <Link to="/Home/Contact" href="javascript:void(0)" class="template-button"><i class="fa fa-angle-double-right" aria-hidden="true"></i> Contact Now</Link> </div>
          </div>
        </div>
      </aside>
        <KonnectSlider/>
        <Footer/>
        </>
        )
}
export default Contact;