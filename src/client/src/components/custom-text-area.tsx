import React from 'react';
import '../App.css';


class CustomTextArea extends React.Component<{}, { value: string }> {
    constructor(props: any) {
        super(props);
        this.state = {
            value: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    
    handleChange(event: any) {
        this.setState({value: event.target.value});
    }

    handleSubmit(event: any) {
        alert('A message was submitted: ' + this.state.value);
        event.preventDefault();
    }

    onSubmitForm = (e: any) => {
        e.preventDefault();
    }

    render() {
        return (
            <form className="custom-text-area-form"  onSubmit={this.onSubmitForm}>
                    <textarea className="custom-text-area" value={this.state.value} onChange={this.handleChange} />
            </form>
        );
    }
}

export default CustomTextArea;