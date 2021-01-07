import React from 'react';
import './style.css';

class TopCard extends React.Component {
  render(){
   return(
    <header className="inner"> 
    {/* Banner */}
    <div className="jumbotron header-content">
      <div className="container">
        <div className="row">
          <div className="col-sm-12">
            <h1 id="homeHeading">Home/ <span> {this.props.pagename} </span></h1>
          </div>
        </div>
      </div>
    </div>
  </header> 
        );
    }
  }

   
  export default TopCard;