import React, { Component } from 'react';
import queryString from 'query-string';
import { ListGroup, ListGroupItem } from 'reactstrap';
import axios from "axios"

const API_URL_ingredientsSearch = "/api/recipes?search=ingredients contains "

export class RecipesResultsListView extends Component {
  

  constructor (props) {
    super(props);

    this.state = {
      ingredientSearched: "",
      resultsingredientSearch:[ ]
    };

    this.handleListEntryClick = this.handleListEntryClick.bind(this);
    this.updateIngredientsSearchResults = this.updateIngredientsSearchResults.bind(this)
  }


  componentDidMount(){
    this.triggerIngredientsSearchQuery();
  }

  handleListEntryClick(item_id){
    this.loadRecipeView(item_id)
  }


  triggerIngredientsSearchQuery(){
    let url = this.props.location.search;
    let params = queryString.parse(url);
    if ("searchTermIngredient" in params){
         this.issueSearchQueryIngredientsSearch(params.searchTermIngredient, this.updateIngredientsSearchResults)
    } 
    else{
      // render nothing
    }
  }

  issueSearchQueryIngredientsSearch(search_term, cb){
    axios.get(API_URL_ingredientsSearch + search_term)
    .then(res => {
      cb(res.data)
    }); 
  }

  updateIngredientsSearchResults(data){
    this.setState({resultsingredientSearch:data.value}); 
  }

  loadRecipeView(item_href){
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
