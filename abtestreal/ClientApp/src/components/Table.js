import React, { Component } from "react";
import { Row } from './Table/Row';
export class Table extends Component {

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            added: [],
            removed: [],
            updated: [],
            newUserId:'',
            newUserRegistered:'',
            newUserLastSeen:'',
            loading: true
        }
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.renderInputLine = this.renderInputLine.bind(this);
        this.handleRemove = this.handleRemove.bind(this);
        this.renderTable = this.renderTable.bind(this);
    }
    
    componentDidMount() {
        this.fetchData();
    }
    
    render() {
        let input = this.renderInputLine();
        let table = this.state.loading ? <p><em>Loading...</em></p> : this.renderTable(this.state.users)
        return <div>
            { table }
            <br/>
            { input }</div>
    }
    renderTable(users) {
        return(
            <table width="100%" cellSpacing="0" border="1">
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Registered</th>
                    <th>Last seen</th>
                    <th> </th>
                    <th> </th>
                </tr>
                </thead>
                <tbody>
                {users.map(user =>
                    <tr key={user.id}>
                        <Row user={user} remove={this.handleRemove}/>
                    </tr>                
                )}
                </tbody>
            </table>
        )
    }
    renderInputLine() {
        return(
            <div>
                <form width="100%">
                    <input className="new-user"
                           type="number"
                        name="newUserId"
                        onChange={this.handleChange}
                        value={this.state.newUserId}
                    />
                    <input className="new-user"
                        type="date"
                        name="newUserRegistered"
                        onChange={this.handleChange}
                        value={this.state.newUserRegistered}
                    />
                    <input className="new-user"
                        type="date"
                        name="newUserLastSeen"
                        onChange={this.handleChange}
                        value={this.state.newUserLastSeen}
                    />
                </form>
                <button onClick={this.handleSubmit}>Add</button>
                <br/>
                <br/>
                <button>Save to Db</button>
            </div>
        )
    }
    
    handleChange(e) {
        this.setState({...this.state, [e.target.name]: e.target.value});
    }
    
   handleRemove(id) {
       this.setState(state => ({users: this.state.users.filter(u => u.id !== id)})) 
    }
    
    handleSubmit(e) {
        e.preventDefault();
        let id = parseInt(this.state.newUserId, 10);
        if(isNaN(id)) return; 
        if(this.state.users.filter(u => u.id === id).length > 0) return;
        if(this.state.newUserRegistered.length === 0 || this.state.newUserLastSeen === 0) return;
        let newUser = {
            id: id,
            registered: this.state.newUserRegistered,
            lastSeen: this.state.newUserLastSeen
        }
        
        this.setState(state => ({users: state.users.concat(newUser)}))
        this.setState( state => ({added: state.added.concat(newUser)}))
        this.setState({newUserId:'', newUserRegistered:'', newUserLastSeen:''})
    }

    async fetchData() {
        const response = await fetch('api/users');
        const rawData = await response.json();
        let data = [];
        rawData.forEach(u => data.push({id: u.id, registered: u.registered.split('T')[0], lastSeen: u.lastSeen.split('T')[0]}))
        this.setState({ users: data, loading: false });
    }

}