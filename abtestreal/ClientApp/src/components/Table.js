import React, { Component } from "react";
import { TableRow } from './TableRow';
export class Table extends Component {

    constructor(props) {
        super(props);
        this.state = {
            users: [],
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
        this.saveToDB = this.saveToDB.bind(this);
        this.handleTableRowChanged = this.handleTableRowChanged.bind(this);
        
    }
    
    componentDidMount() {
       setTimeout( () => {
           this.setState({users: [...this.props.fetchedUsers]});
       }, 1000)
        
    }

    render() {
        let input = this.renderInputLine();
        let table = this.renderTable(this.state.users)
        let saveBtn = <button className="btn btn-primary" onClick={this.saveToDB}>Save to Db</button>
        return (<>
            { table }
            <div className="container input">
                { input }
                { saveBtn }
            </div>            
            </>)
    }
    
    renderTable(users) {
        return(
            <table cellSpacing="0" border="1">
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Registered</th>
                    <th>Last seen</th>
                    <th> </th>
                </tr>
                </thead>
                <tbody>
                {users.map(user =>
                    <tr key={user.id}>
                        <TableRow user={user} handleTableRowChanged={this.handleTableRowChanged} remove={this.handleRemove}/>
                    </tr>                
                )}
                </tbody>
            </table>
        )
    }
    renderInputLine() {
        return(
            <div className="new-row">
                <form>
                    <input className="new-user"
                           type="number"
                        name="newUserId"
                           placeholder="id"
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
                <button className="btn btn-add" onClick={this.handleSubmit}>Add</button>
            </div>
        )
    }
    
    handleChange(e) {
        this.setState({...this.state, [e.target.name]: e.target.value});
    }
    
   handleRemove(id) {
       this.setState(state => ({users: this.state.users.filter(u => u.id !== id)}))
    }
    
    handleTableRowChanged(user) {        
        let updatedUsers = this.state.users.map(u => (u.id === user.id) ? user : u);
        this.setState(state => ({users: updatedUsers}));        
}
    
    handleSubmit(e) {
        e.preventDefault();
        let id = parseInt(this.state.newUserId, 10);
        if(isNaN(id)) return; 
        if(this.state.users.filter(u => u.id === id).length > 0) return;
        if(this.state.newUserRegistered.length === 0 || this.state.newUserLastSeen === 0) return;
        if(this.state.newUserRegistered > this.state.newUserLastSeen) return;
        let newUser = {
            id: id,
            registered: this.state.newUserRegistered,
            lastSeen: this.state.newUserLastSeen
        }
        
        this.setState(state => ({users: state.users.concat(newUser)}))
        this.setState({newUserId:'', newUserRegistered:'', newUserLastSeen:''})
    }
    
    saveToDB() {      
        
        let added = this.state.users.filter(u => !this.props.fetchedUsers.some(fu => u.id === fu.id));
        let removed = this.props.fetchedUsers.filter(u => !this.state.users.some(fu => u.id === fu.id));
        let notUpdated = this.props.fetchedUsers.filter(u => this.state.users.includes(u));
        let updated = this.state.users.filter(u => this.props.fetchedUsers.some(fu => u.id === fu.id)).filter(u => !notUpdated.some(nu => nu.id === u.id));
        
       this.props.onSave({added, updated, removed});
                
    }    
}