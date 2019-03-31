import React, { Component } from 'react';
import queryString from 'query-string';
import { ListGroup, ListGroupItem, Group} from 'reactstrap';

export class RecipesResultsListView extends Component {
  

  constructor (props) {
    super(props);
    this.state = {
      results_ingredientSearch: []
    };
  }


  componentDidMount(){
      let url = this.props.location.search;
      let params = queryString.parse(url);
      if ("searchTermIngredient" in params){
           this.issue_searchQuery_ingredientsSearch(params.searchTermIngredient)
      } 
      else{
        // render nothing
      }
  }

  issue_searchQuery_ingredientsSearch(search_term){
    //backendfest
    console.log("search term received: "+ search_term)
  }

  create_ui_entry_result(item){
    // create the entries ui template (title)
    <ListGroupItem>Vestibulum at eros</ListGroupItem>
  }
  

  create_ui_list_results(result_entries){
    if (result_entries.length>0){
      var ui_entryItems_results = result_entries.map(this.create_ui_entry_result)
      return (
      <div>{ui_entryItems_results}</div>
    )}
    else{
      return( <div>no entries</div>)
    }
  }


  render () {
    return (
      <div>
        <h1>Results</h1>
        <ListGroup>
        {this.create_ui_list_results(this.state.results_ingredientSearch)}
        </ListGroup>
      </div>
    );
  }
}
