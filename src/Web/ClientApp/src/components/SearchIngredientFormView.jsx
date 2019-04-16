import React, { Component } from 'react';
import { Form, Button, Label, Input } from 'reactstrap';

export class SearchIngredientFormView extends Component {

  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.LINK_SEARCHPAGE = '/results?searchTermIngredient=';
  }

  // IGTest: serachIngredient was triggered
  handleSubmit(e) {
    console.log(this.inputtaskValue.value);
    if (this.inputtaskValue.value !== "") {
    
    this.triggerIngredientSearch(this.inputtaskValue.value);
    }
    e.preventDefault();
  }

  triggerIngredientSearch(searchTerm) {
      window.location.href=this.LINK_SEARCHPAGE +""+ searchTerm;
  }

  // unit: comp renders as expected
  render() {
    return (
      <div>
        <Form onSubmit={this.handleSubmit}>
          <Label for="form_input">Search by ingredient</Label>
          <Input type="text" id="input" className = "input" placeholder="Enter an ingredient you have" innerRef={(i) => this.inputtaskValue = i} />
          <Button id = "btn" className = "btn">Search</Button>
        </Form>
      </div>
    );
  }
}
