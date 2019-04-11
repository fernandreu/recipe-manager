import React, { Component } from 'react';
import queryString from 'query-string';
import { Label, Container, Row, ListGroup, ListGroupItem } from 'reactstrap';
import axios from "axios"

const API_URL_recipe_id = "/api/recipes/"


export class RecipeDetailsView extends Component {

  constructor(props) {
    super(props);
    this.state = {
      recipe_details: []
    };

    this.updateRecipeDetails = this.updateRecipeDetails.bind(this);
  }

  componentDidMount() {
    this.triggerRecipeDetailsQuery();
  }

  

  triggerRecipeDetailsQuery() {
    let url = this.props.location.search;
    let params = queryString.parse(url);
    if ("id" in params) {
      this.query_search_recipe_details(params.id, this.updateRecipeDetails)
    }
    else {
      // render nothing
    }
  }

  updateRecipeDetails(data) {
    this.setState({ recipe_details: data });
  }

  query_search_recipe_details(search_term, cb) {
    axios.get(API_URL_recipe_id + search_term)
      .then(res => {
        cb(res.data)
      });
  }

  create_ui_entry_ingredient(item) {
    <ListGroupItem> {item.name} </ListGroupItem>
  }

  //todo: add ingredients overview as table
  create_ui_recipe_view(item) {
    //var ingredients = item.ingredients.map(this.create_ui_entry_ingredient, this) 
    return (
      <Container>
        <Row>
          <Label type="text">{item.title}</Label>
        </Row>
        <Row>
          <Label type="text">{item.details}</Label>
        </Row>
        <Row className="ingredients">

        </Row>
      </Container>
    )
  }

  render() {
    return (
      <div>
        <h1>Recipe</h1>
        {this.create_ui_recipe_view(this.state.recipe_details)}
      </div>
    );
  }
}
