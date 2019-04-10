import React, { Component } from 'react';
import { Form, Button, Label, Input } from 'reactstrap';

export class SearchIngredientFormView extends Component {

  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  // IGTest: serachIngredient was triggered
  handleSubmit(e) {
    this.triggerIngredientSearch();
    e.preventDefault();
  }

  triggerIngredientSearch() {
    if (this.inputtaskValue.value !== "") {
      var searchTerm = this.inputtaskValue.value
      window.location.assign('/results?searchTermIngredient=' + searchTerm);
    }
  }

  // unit: comp renders as expected
  render() {
    return (
      <div>
        <Form onSubmit={this.handleSubmit}>
          <Label for="form_input">Search by ingredient</Label>
          <Input type="text" name="text" placeholder="Enter an ingredient you have" innerRef={(i) => this.inputtaskValue = i} />
          <Button>Search</Button>
        </Form>
      </div>
    );
  }
}
