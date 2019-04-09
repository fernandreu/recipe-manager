import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { SearchIngredientFormView} from './components/SearchIngredientFormView';
import {RecipesResultsListView} from "./components/RecipesResultsListView";
import {RecipeDetailsView} from "./components/RecipeDetailsView";

export default class App extends Component {
  static displayName = App.name;

  constructor (props) {
    super(props);

  }

  render () {
    return (
      <div><h2>Welcome to React</h2>
      //structure: 
      // - headerlabel
      // |
      // --- form: input , btn
      // --- restults list
      // --- recipe overview
      
      <Layout>
        <Route exact path='/' component = {SearchIngredientFormView} />
        <Route exact path= "/results"  component ={RecipesResultsListView} />
        <Route exact path= "/recipe" component = {RecipeDetailsView}/>
      </Layout>
      </div>
    );
  }
}
