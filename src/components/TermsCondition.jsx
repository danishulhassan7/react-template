import React from "react"


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const TermsCondition=()=>{
    return(
      <>
      <TopBar/>
      <Nav/>
        <div class="header-content">
        <div class="container">
          <div class="row">
            <div class="col-sm-12">
              <h1 id="homeHeading"><a href="index-2.html">Home</a> / <span>Terms & Conditions</span></h1>
            </div>
          </div>
        </div>
      </div>
      <section>
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2 class="para-heading">Terms</h2>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum. Nam risus purus, hendrerit id placerat sit amet, tempor a urna. Maecenas id quam et dolor facilisis pulvinar.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
        <p>Curabitur ut est a mi fermentum tristique. Aliquam et ante odio. Donec elementum odio eget ex porta, vel laoreet nisl fermentum. Nam risus purus, hendrerit id placerat sit amet, tempor a urna. Maecenas id quam et dolor facilisis pulvinar.</p>
      </div>
    </div>
  </div>
</section>
<KonnectSlider/>
<Footer/>
</>
    )
}
export default TermsCondition;