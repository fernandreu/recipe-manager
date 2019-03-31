import React, { Component } from 'react';
import queryString from 'query-string';
import { ListGroup, ListGroupItem } from 'reactstrap';

const API_URL_ingredientsSearch = "https://localhost:5001/api/recipes?search=ingredients contains "

export class RecipesResultsListView extends Component {
  

  constructor (props) {
    super(props);
    this.handleListEntryClick = this.handleListEntryClick.bind(this);
    this.state = {
      ingredientSearched: "",
      resultsingredientSearch:[ ]
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
    var x = []
    fetch(API_URL_ingredientsSearch + search_term)
      .then(response => response.json())
      .then((data) => { this.setState({resultsingredientSearch:data.value}); })
  }


  handleListEntryClick(item_id){
    this.load_recipe_view(item_id)
  }


  load_recipe_view(item_href){
    var item_href_str_parts = item_href.split("/");
    var item_id= item_href_str_parts[item_href_str_parts.length-1 ]
    window.location.assign('/recipe?id='+item_id);
  }


  create_ui_entry_result(item){
    return(
    <ListGroupItem className="userLabel_empty" key={item.href} onClick={()=> {this.handleListEntryClick(item.href)}}>{item.title}</ListGroupItem>
    )
  }
  

  create_ui_list_results(result_entries){
    if (result_entries.length>0){
      var ui_entryItems_results = result_entries.map(this.create_ui_entry_result, this)
      return (
        <ListGroup>
          {ui_entryItems_results}
      </ListGroup>
    )}
    else{
      return( <div>no entries</div>)
    }
  }


  render () {
    return (
      <div>
        <h1>Results</h1>
        
        {this.create_ui_list_results(this.state.resultsingredientSearch)}
   
      </div>
    );
  }


}
