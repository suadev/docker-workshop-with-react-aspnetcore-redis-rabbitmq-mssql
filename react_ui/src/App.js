import React, { Component } from 'react';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      redisKeys: []
    }

    this.sendToRedis = this.sendToRedis.bind(this);
    this.getFromRedis = this.getFromRedis.bind(this);
  }

  componentDidMount() {
    this.getFromRedis();
  }

  sendToRedis(e) {
    let self = this;
    e.preventDefault();

    fetch('http://localhost:5000/api/redis/', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        key: this.keytosave.value,
        value: this.valuetosave.value
      })
    })
      .then(function (response) {
        self.getFromRedis()
      })
  }

  getFromRedis() {
    let self = this;
    fetch('http://localhost:5000/api/redis/', {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      }
    })
      .then(function (response) {
        return response.json();
      })
      .then(function (json) {
        debugger;
        self.setState({
          redisKeys: json
        })
      })
      .catch(function () {
        console.log("error");
      });
  }

  render() {
    let redisKeys = this.state.redisKeys;
    debugger;
    console.log(redisKeys);
    return (
      <div>
        <form onSubmit={this.sendToRedis}>
          <h1>Redis Form</h1>
          <label>
            <b>Key:</b>
            <input type="text" ref={(key) => this.keytosave = key} />
          </label>
          <label>
            <b>Value:</b>
            <input type="text" ref={(value) => this.valuetosave = value} />
          </label>
          <input type="submit" value="Save to Redis" />
        </form>
        <br />
        <h2>Dynamically updated Redis Key List</h2>
        <ul>
          {this.state.redisKeys.map(function (item, index) {
            return <li key={index}>{item.key} - > {item.value}</li>;
          })}
        </ul>
        <br />
        <hr />
        <br />

        <form onSubmit={this.sendToRedis}>
          <h1>MsSQL Form</h1>
          {/* <label>
            <b>Key:</b>
            <input type="text" ref={(key) => this.keytosave = key} />
          </label>
          <label>
            <b>Value:</b>
            <input type="text" ref={(value) => this.valuetosave = value} />
          </label> */}
          <input type="submit" value="Save to MsSQL" />
        </form>
      </div>
    );
  }
}
export default App;