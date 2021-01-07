import React from "react";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
import "../css/animate.css";
import "../css/theme.css";
 
import "../assets/bootstrap/css/bootstrap.css";
import "../css/konnect-slider.css";
import "../css/green.css";
import "../assets/font-awesome/css/font-awesome.min.css";

// import News1 from "../img/news/news1.jpg"
// import News2 from "../img/news/news2.jpg"
// import News3 from "../img/news/news3.jpg"


export default function SingleCourse(props) {
  // Used this method so that for backend this method can be used to extract the course id and used appropriate data
  const str = window.location.href.split("/");
  const image = str[str.length - 1];
    return (
        <>
        <TopBar/>
        <Nav/>
 <header className="inner"> 
  <div className="header-content">
    <div className="container">
      <div className="row">
        <div className="col-sm-12">
          <h1 id="homeHeading">Home / <span>Single Course</span></h1>
        </div>
      </div>
    </div>
  </div>
</header>

<section class="section-bottom-border">
  <div class="container">
    <div class="row">
      <div class="col-md-8 list-container post"> 
        {/* <!-- Course Social Sharing icons --> */}
        <div class="post-social-share"> <span>Share On</span> <Link to="javascript:void(0)"><i class="fa fa-facebook fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-twitter fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-google-plus fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-whatsapp fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-linkedin fa-2x" aria-hidden="true"></i></Link> </div>
        {/* <!-- Course description --> */}
        {/* <hr> */}
        <img src={require("../img/news/"+image+".jpg")} class="img-responsive" alt="" />
      
        <p>Sed a pulvinar risus. Donec aliquam tincidunt nunc, eget eleifend eros maximus a. Praesent quis quam eros. Mauris tristique leo a lorem auctor, vel rutrum eros tempor. Donec fringilla nunc enim, sed posuere velit pellentesque ut. Integer faucibus massa vitae magna ullamcorper, vulputate sagittis metus dictum. Donec lobortis, lorem in vehicula eleifend, velit lectus viverra libero, et tincidunt augue ante quis velit. Donec in volutpat diam.</p>
        <h2 class="single-post-heading">Heaging comes here</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus a consequat mi. Mauris ullamcorper lacinia elit, at porttitor elit facilisis nec. Sed eleifend ornare turpis ac scelerisque. Donec in euismod erat. Cras luctus dapibus nibh, ac tincidunt magna semper sit amet. Nullam ligula lectus, convallis nec turpis id, efficitur convallis diam. Nunc ut leo tincidunt, interdum neque vitae, pulvinar nunc. Mauris quis placerat dui, non commodo tortor. Duis quis dolor massa. In placerat molestie facilisis. Sed id tempus nulla. In blandit porta mi, vel accumsan eros consequat eget.</p>
        {/* <hr> */}
        {/* <!-- Course social sharing icons --> */}
        <div class="post-social-share"> <span>Share On</span> <Link to="javascript:void(0)"><i class="fa fa-facebook fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-twitter fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-google-plus fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-whatsapp fa-2x" aria-hidden="true"></i></Link> <Link to="javascript:void(0)"><i class="fa fa-linkedin fa-2x" aria-hidden="true"></i></Link> </div>
        {/* <hr> */}
      </div>
      {/* <!-- ==== Sidebar Starts Here ==== --> */}
      <div class="col-md-4 sidebar"> 
        
        {/* <!--Sidebar Course details--> */}
        <h2>Course Details</h2>
        <ul class="ul-course">
          <li><span>Course</span>: PHP</li>
          <li><span>Price</span>: $120</li>
          <li><span>Duration</span>: 3 to 4 Weeks</li>
          <li><span>Type</span>: ClassRoom</li>
          <li><span>Faculty</span>: Harry Bodd</li>
        </ul>
        <Link to="/ApplyCourse" class="service-box-button">Apply</Link>
        {/* <hr> */}
        {/* <!--Sidebar Courses--> */}
        <h2>Other Courses</h2>
        <ul class="other-courses">
          <li>
            <div class="article-box">
              <article>
                <div class="news-post">
                  <div class="img-box"> <span>$150</span><Link to="#"><img src={require("../img/news/news1.jpg")} alt="it's me Image" /></Link> </div>
                  <div class="post-content-text">
                  <h4><span>Course One</span></h4>
                  <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
                  <div class="post-more"><Link to="SingleCourse">Attend</Link> </div>
                </div>
              </div>
            </article>
          </div>
        </li>
        <li>
          <div class="article-box">
            <article>
              <div class="news-post">
                <div class="img-box"> <span>$150</span><Link to="#"><img src={require("../img/news/news1.jpg")} alt="it's me Image" /></Link> </div>
                <div class="post-content-text">
                  <h4><span>Course One</span></h4>
                  <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
                  <div class="post-more"><Link to="SingleCourse">Attend</Link> </div>
                </div>
              </div>
            </article>
          </div>
        </li>
      </ul>
      {/* <hr> */}
    </div>
    {/* <!-- ==== Sidebar Ends Here ==== -->  */}
  </div>
</div>
</section>
<KonnectSlider/>
<Footer/>
</>
)
}
{/* <script>
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','../../www.google-analytics.com/analytics.js','ga');

ga('create', 'UA-77800499-1', 'auto');
ga('send', 'pageview');

</script> */}