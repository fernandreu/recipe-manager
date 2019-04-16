import React, { Component } from "react";
import queryString from "query-string";
import { Label, Container, Row, ListGroup, ListGroupItem } from "reactstrap";
import axios from "axios";



export class RecipeDetailsView extends Component {
  constructor(props) {
    super(props);
    this.state = {
      recipe_details: []
    };
    this.API_URL_recipe_id = "/api/recipes/";
    this.updateRecipeDetails = this.updateRecipeDetails.bind(this);
  }

  componentDidMount() {
    this.triggerRecipeDetailsQuery();
  }

  triggerRecipeDetailsQuery() {
    let url = "";
    let params = "";
    try {
      url = this.props.location.search;
      params = queryString.parse(url);
    } catch (error) {
      //TODO: issue error
    }

    if ("id" in params) {
  
      this.query_search_recipe_details(params.id, this.updateRecipeDetails);
    } else {
      // render nothing
    }
  }

  updateRecipeDetails(data) {
    this.setState({ recipe_details: data });
  }

  query_search_recipe_details(search_term, cb) {

    axios.get(this.API_URL_recipe_id + search_term).then(res => {
      cb(res.data); 
    }).catch(error => {
      console.log(error.response)
      //TODO:test: raise error
  });
  }

  create_ui_entry_ingredient(item) {
    <ListGroupItem> {item.name} </ListGroupItem>;
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
        <Row className="ingredients" />
      </Container>
    );
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
