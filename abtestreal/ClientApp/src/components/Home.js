import React, { Component } from "react";
import { Table } from './Table'
import {Bar, BarChart, Label, XAxis, YAxis, Tooltip, ResponsiveContainer, CartesianGrid} from "recharts";


export class Home extends Component {    
    constructor(props) {
        super(props);
        this.state = {
            users: [],
            loading: true,
            chartData: [],
            rollingRetention: {days: 7, percent:''}
        }
        this.saveToDb = this.saveToDb.bind(this);
        this.calculate = this.calculate.bind(this);
    }

    componentDidMount() {
        this.fetchData();
    }
    
    render() {
        let table = this.state.loading
            ? (<p><em>Loading...</em></p>)
            : (<Table fetchedUsers={this.state.users} onSave={this.saveToDb}/>);
        let calculateBtn = <button className="btn btn-primary" onClick={this.calculate}>Calculate</button>
        let RollingRetention = this.state.chartData.length === 0 ? <></> : <p>Rolling Retention {this.state.rollingRetention.days} days: {this.state.rollingRetention.percent}</p>
        let chart = this.state.chartData.length === 0
            ? <></>
            :<ResponsiveContainer width="100%" minHeight={700}>
                
                <BarChart
                          data={this.state.chartData}
                          margin={{top: 5, right: 30, left: 20, bottom: 5}}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="days" label="Days"/>
                    <YAxis interval={1}>
                        <Label value="Users" angle={-90} position="insideLeft"/>
                    </YAxis>
                    <Bar  dataKey="count" fill="#82ca9d"/>
                </BarChart>
            </ResponsiveContainer> 
        return(
            <div>
                <div>{table}</div>
                <div>{calculateBtn}</div>
                <div>{RollingRetention}</div>
                <div>{chart}</div>
            </div>            
        )
    }
    
    async fetchData() {
        const response = await fetch('api/users');
        const responseBody = await response.json();
        let data = [];
        responseBody.items.forEach(u => data.push({id: u.id, registered: u.registered.split('T')[0], lastSeen: u.lastSeen.split('T')[0]}))
        this.setState({ users: data, loading: false });
    }
    
   async saveToDb(data) {
        this.setState({loading: true});
        if(data.removed.length > 0) {
           await this.sendData("delete", {users: data.removed});
        }
        
        if(data.added.length > 0) {
           await this.sendData("post", {users: data.added});
        }
        
        if(data.updated.length > 0) {
           await this.sendData("put", {users: data.updated});
        }
      this.fetchData();
    }
    
    sendData(method, data) {
        fetch('/api/users', {
            method: method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(res => {
            if (!res.ok) {
                window.alert("Error during " + method + " users: " + res.statusText);
            }
        });
    }
    async calculate(){
       await fetch('/api/users/lifetime/summary').then(res => {
            if(res.ok) {
                res.json().then(body => this.setState(state => ({chartData: body.items})))
            } else { window.alert("Error during get users lifetime statistics: " + res.statusText)}
        })
        
        await fetch('api/users/rollingretention/' + this.state.rollingRetention.days).then(res => {
            if(res.ok) {
                res.json().then(body => this.setState(state => ({rollingRetention: body})))
            } else { window.alert("Error during get rolling retention: " + res.statusText)}
        })
    }
}