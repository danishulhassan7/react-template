import React from "react";


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

import "../css/animate.css";
import "../css/theme.css";

import "../assets/bootstrap/css/bootstrap.css";
import "../css/konnect-slider.css";
import "../css/green.css";

import News1 from "../img/news/news1.jpg"
import News2 from "../img/news/news2.jpg"
import News3 from "../img/news/news3.jpg"

export default function Course() {
    return (
      <>
<TopBar/>
<Nav/>
<section className="section-bottom-border">
  <div className="container">
  <header class="inner"> 

  <div class="header-content">
  <div class="container">
    <div class="row">
      <div class="col-sm-12">
        <h1 id="homeHeading"><Link to="/">Home</Link> / <span>Course</span></h1>
      </div>
    </div>
  </div>
  </div>
</header>
    <div className="row"> 
      {/* <!--Course One--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$150</span><Link to="#"><img src={News1} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course One</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news1">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course Two--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$110</span><Link to="#"><img src={News2} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Two</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news2">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course Three--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$90</span><Link to="#"><img src={News3} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Three</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 4-5 Weeks</h4> */}
              <div className="post-more" >
              <Link to="/Home/SingleCourse/news3">Attendd</Link> 
              
              
              </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course four--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$110</span><Link to="#"><img src={News2} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Four</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news2">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course five--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$90</span><Link to="#"><img src={News3} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Five</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 4-5 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news3">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      {/* <!--Course six--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$150</span><Link to="#"><img src={News1} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Six</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news1">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      {/* <!--Course seven--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$150</span><Link to="#"><img src={News1} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Seven</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news1">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course eight--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$110</span><Link to="#"><img src={News2} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Eight</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 3-4 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news2">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
      
      {/* <!--Course nine--> */}
      <div className="col-sm-4 article-box">
        <article>
          <div className="news-post">
            <div className="img-box"> <span>$90</span><Link to="#"><img src={News3} alt="it's me Image" /></Link> </div>
            <div className="post-content-text">
              <h4><span>Course Nine</span></h4>
              <h4><i class="fa fa-calendar-check-o" aria-hidden="true"></i> 3-4 Weeks</h4>
              {/* <h4><Icon icon={calendarCheckO} /> 4-5 Weeks</h4> */}
              <div className="post-more"><Link to="SingleCourse/news3">Attend</Link> </div>
            </div>
          </div>
        </article>
      </div>
    </div>
  </div>
</section>
<KonnectSlider/>
<Footer/>
</>
    )
}