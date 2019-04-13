import React, { Component } from "react";
import queryString from "query-string";
import { ListGroup, ListGroupItem } from "reactstrap";
import axios from "axios";

export class RecipesResultsListView extends Component {
  constructor(props) {
    super(props);

    this.state = {
      ingredientSearched: "",
      resultsingredientSearch: [],
      noResultsFound: false
    };

    this.API_URL_ingredientsSearch =
      "/api/recipes?search=ingredients contains ";
    this.API_URL_RECIPE_EP = "/recipe?id=";

    this.handleListEntryClick = this.handleListEntryClick.bind(this);
    this.updateIngredientsSearchResults = this.updateIngredientsSearchResults.bind(
      this
    );
  }

  componentDidMount() {
    this.setState({ noResultsFound: false });
    this.triggerIngredientsSearchQuery();
  }

  handleListEntryClick(itemKey) {
    // NB: itemKey is a URL with the unique ID at its end. Example: https://localhost:5001/api/recipes/b13ce333-307f-4995-bd88-33d7280d785d"
    if (itemKey.includes("/")) {
      //TODO: advise to change itemKey convention to only feature ID, not full URL
      var itemKey_str_parts = itemKey.split("/");
      var item_id = itemKey_str_parts[itemKey_str_parts.length - 1];
      this.loadRecipeView(item_id);
    } else {
      //TODO: Error bad format
    }
  }

  triggerIngredientsSearchQuery() {
    let url = this.props.location.search;
    let params = queryString.parse(url);

    if ("searchTermIngredient" in params) {
      if (params["searchTermIngredient"] != ""){
      this.queryIngredientsSearch(
        params.searchTermIngredient,
        this.updateIngredientsSearchResults
      );
    }
    } else {
      // raise error: no request string
    }
  }

  queryIngredientsSearch(search_term, cb) {
    axios.get(this.API_URL_ingredientsSearch + search_term).then(res => {
      cb(res.data);
    }).catch(error => {
      console.log(error.response)
  });
  }

  updateIngredientsSearchResults(data, cb = () => {}) {
    if ("value" in data) {
      if (data.value.length > 0) {
        this.setState({ resultsingredientSearch: data.value }, cb());
      } else {
        this.setState({ noResultsFound: true });
      }
    } else {
      //TODO: raise error: data incorrectly formatted, "value" prop is missing
    }
  }

  loadRecipeView(item_id) {
    window.location.href = this.API_URL_RECIPE_EP + item_id;
  }

  create_ui_entry_result(item) {
    if ("title" in item) {
      return (
        <ListGroupItem
          className="entry"
          key={item.href}
          onClick={() => {
            this.handleListEntryClick(item.href);
          }}
        >
          {item.title}
        </ListGroupItem>
      );
    } else {
      //TODO: raise error: item incorrectly formatted
    }
  }

  create_ui_list_results(result_entries) {
    if (result_entries.length > 0) {
      var ui_entryItems_results = result_entries.map(
        this.create_ui_entry_result,
        this
      );
      return <ListGroup>{ui_entryItems_results}</ListGroup>;
    }
  }

  render() {
    if (this.state.noResultsFound === true) {
      return <div>no entries</div>;
    } else {
      return (
        <div>
          <h1>Results</h1>
          {this.create_ui_list_results(this.state.resultsingredientSearch)}
        </div>
      );
    }
  }
}
