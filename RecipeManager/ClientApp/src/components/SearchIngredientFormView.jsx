import React, { Component } from 'react';
import { Form, Button, Label, Input } from 'reactstrap';

export class SearchIngredientFormView extends Component {


  constructor (props) {
    super(props);
    this.searchIngredient = this.searchIngredient.bind(this);
  }


  searchIngredient(e){
    e.preventDefault();
    console.log (this.inputtaskValue.value)
    if (this.inputtaskValue.value !== "") 
    {
      var searchTerm = this.inputtaskValue.value
      window.location.assign('/results?searchTermIngredient='+searchTerm);
    }
    e.preventDefault(); 
  }


  render () {
    return (
      <div>
        <Form onSubmit={this.searchIngredient}>
            <Label for="form_input">Search by ingredient</Label>
            <Input type="text" name="text" placeholder="Enter an ingredient you have" innerRef={(i) => this.inputtaskValue = i}  />
            <Button>Search</Button>
        </Form>
      </div>
    );
  }


}
