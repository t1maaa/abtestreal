import React, { Component } from 'react';

export class TableRow extends Component {

    constructor(props) {
        super(props);
        this.state = {
            id:this.props.user.id,
            registered:this.props.user.registered,
            lastSeen:this.props.user.lastSeen,
            editing: false
        };
        this.handleChange = this.handleChange.bind(this);
    }
    
    render() {
        let isEditing = (<>
                <td>{this.state.id}</td>
                <td>{<input type="date" name="registered"  value={this.state.registered} onChange={this.handleChange}/>}</td>
                <td>{<input type="date" name="lastSeen" value={this.state.lastSeen} onChange={this.handleChange}/>}</td>
                <td align="center"><button className="btn btn-td" onClick={ () => this.editHandle()}>Save</button><button className="btn btn-td" onClick={ () => this.props.remove(this.state.id)}>Remove</button></td>
            </>)
        
        let row = (<>
            <td>{this.state.id}</td>
            <td>{this.state.registered}</td>
            <td>{this.state.lastSeen}</td>
            <td align="center"><button className="btn btn-td" onClick={ () => this.editHandle()}>Edit</button><button className="btn btn-td" onClick={ () => this.props.remove(this.state.id)}>Remove</button></td>
        </>);
        return this.state.editing ? isEditing : row;
    }

    handleChange(e) {
        this.setState({...this.state, [e.target.name]: e.target.value});
    }
    editHandle() {
        if(!this.state.editing)
            this.setState({editing: true});
        else {
            if(this.state.registered <= this.state.lastSeen)
            {
                this.setState({editing: false});
                this.props.handleTableRowChanged({id: this.state.id, registered: this.state.registered, lastSeen: this.state.lastSeen})
            }            
        }
    }
}
