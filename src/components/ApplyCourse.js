import React from "react"

import { Link } from 'react-router-dom';
import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const ApplyCourse=()=>{

    return(
        <>
        <TopBar />
        <Nav />
        <section class="section-bottom-border login-signup">
        <header class="inner"> 

  <div class="header-content">
  <div class="container">
    <div class="row">
      <div class="col-sm-12 mb-5">
        <h1 id="homeHeading"><Link to="/">Home</Link> / <span>Apply Course</span></h1>
      </div>
    </div>
  </div>
  </div>
</header>
  <div class="container">
    <div class="row">
      <div class="login-main template-form">
        <h2>Apply Course</h2>
        <div class="template-space"></div>
        <form>
          <div class="form-group">
            <label for="inputEmail">Select Course</label>
            <select class="form-control">
              <option>PHP</option>
              <option>Physics</option>
              <option>Html</option>
              <option>jQuery</option>
            </select>
          </div>
          <div class="form-group">
            <label for="inputUsername">Your Name</label>
            <input id="inputUsername" type="text" class="form-control">
         </input> </div>
         <div class="form-group">
            <label for="inputEmail">Your Email</label>
            <input id="inputEmail" type="text" class="form-control">
       </input>   </div>
          <div class="form-group">
            <label for="inputPhone">Phone Number</label>
            <input id="inputPhone" type="text" class="form-control">
        </input>  </div>
          <div class="form-group">
            <label for="inputNote">Note</label>
            <textarea id="inputNote" class="form-control"></textarea>
          </div>
          <button type="submit" class="btn btn btn-primary"> Apply </button>
        </form>
      </div>
    </div>
  </div>
</section>
      <KonnectSlider/>
      <Footer/>  
        </>)
}
export default ApplyCourse;