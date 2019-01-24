import React, { Component } from 'react';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      redisKeys: [],
      todoList: [],
      logs: []
    }

    this.sendToRedis = this.sendToRedis.bind(this);
    this.getFromRedis = this.getFromRedis.bind(this);
    this.sendToMssql = this.sendToMssql.bind(this);
    this.getFromMssql = this.getFromMssql.bind(this);
    this.getLogs = this.getLogs.bind(this);
  }

  componentDidMount() {
    this.getFromRedis();
    this.getFromMssql();
    this.getLogs();
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
        key: this.rediskey.value,
        value: this.redisvalue.value
      })
    })
      .then(function (response) {
        self.getFromRedis();
        self.getLogs();
      })
  }

  sendToMssql(e) {
    let self = this;
    e.preventDefault();

    fetch('http://localhost:5000/api/mssql/', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        description: this.tododescription.value,
        deadline: this.tododeadline.value
      })
    })
      .then(function (response) {
        self.getFromMssql();
        self.getLogs();
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
        self.setState({
          redisKeys: json
        })
      })
      .catch(function () {
        console.log("error");
      });
  }

  getFromMssql() {
    let self = this;
    fetch('http://localhost:5000/api/mssql/', {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      }
    })
      .then(function (response) {
        return response.json();
      })
      .then(function (json) {
        self.setState({
          todoList: json
        })
      })
      .catch(function () {
        console.log("error");
      });
  }

  getLogs() {
    let self = this;
    fetch('http://localhost:5000/api/logs/', {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      }
    })
      .then(function (response) {
        return response.json();
      })
      .then(function (json) {
        self.setState({
          logs: json
        })
      })
      .catch(function () {
        console.log("error");
      });
  }

  render() {
    return (
      <div>
        <form onSubmit={this.sendToRedis}>
          <h1>Redis Container - Random Key/Value</h1>
          <label>
            <b>Key:</b>
            <input type="text" ref={(key) => this.rediskey = key} />
          </label>
          <label>
            <b>Value:</b>
            <input type="text" ref={(value) => this.redisvalue = value} />
          </label>
          <input type="submit" value="Save to Redis" />
        </form>
        <br />
        <ul>
          {this.state.redisKeys.map(function (item, index) {
            return <li key={index}>{item.key} - > {item.value}</li>;
          })}
        </ul>
        <br />
        <hr />
        <br />
        <form onSubmit={this.sendToMssql}>
          <h1>MS-SQL Container - Todo List</h1>
          <label>
            <b>What to do?:</b>
            <input type="textarea" ref={(description) => this.tododescription = description} />
          </label>
          <label>
            <b>Deadline:</b>
            <input type="date" ref={(deadline) => this.tododeadline = deadline} />
          </label>
          <input type="submit" value="Save to MsSQL" />
        </form>
        <br />
        <ul>
          {this.state.todoList.map(function (item, index) {
            return <li key={index}>{item.description} - > {item.deadLine}</li>;
          })}
        </ul>
        <br />
        <hr />
        <br />
        <h1>RabbitMQ Container - Logs</h1>
        <br />
        {this.state.logs && this.state.logs.map(function (item, index) {
          return <li key={index}>{item}</li>;
        })}
      </div>
    );
  }
}
export default App;