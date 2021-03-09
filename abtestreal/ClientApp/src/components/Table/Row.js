import React, { Component } from 'react';

export class Row extends Component {

    constructor(props) {
        super(props);
        this.state = {
            id:this.props.user.id,
            registered:this.props.user.registered,
            lastSeen:this.props.user.lastSeen,
            
        };
    }
    
    render() {
        return (<>
                <td>{this.state.id}</td>
                <td>{this.state.registered}</td>
                <td>{this.state.lastSeen}</td>
                <td><button>Edit</button></td>
                <td><button onClick={() => this.props.remove(this.state.id)}>Remove</button></td>
        </>)
    }

}