import React, { Component } from 'react';
import queryString from 'query-string';
import { Label, Container, Row, ListGroup, ListGroupItem } from 'reactstrap';

const API_URL_recipe_id = "https://localhost:5001/api/recipes/"

//DELETE THIS COMMENT: just for testing 2

export class RecipeDetailsView extends Component {
  

  constructor (props) {
    super(props);

    this.state = {
      recipe_details:[]
    };
  }

  
  componentDidMount(){
      let url = this.props.location.search;
      let params = queryString.parse(url);
      if ("id" in params){
           this.issue_searchQuery_recipe_details(params.id)
      } 
      else{
        // render nothing
      }
  }


  issue_searchQuery_recipe_details(search_term){
    fetch(API_URL_recipe_id + search_term)
      .then(response => response.json())
      .then((data) => {this.setState({recipe_details:data}); 
      }
    )
  }

  create_ui_entry_ingredient(item)
  {
    <ListGroupItem> {item.name} </ListGroupItem>


  }
  //todo: add ingredients overview as table
  create_ui_recipe_view(item){
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


  render () {
    return (
      <div>
        <h1>Recipe</h1>
        {this.create_ui_recipe_view(this.state.recipe_details)}
      </div>
    );
  }


}
