import React from "react";
import Button from "react";
import ReactDOM from "react-dom";
import { MemoryRouter } from "react-router-dom";
import App from "../App";
import { shallow, mount } from "enzyme";
import TestRenderer from 'react-test-renderer';
import axios from 'axios';
import { SearchIngredientFormView } from "../components/SearchIngredientFormView";
import { RecipesResultsListView } from "../components/RecipesResultsListView";

beforeEach(() => {
  return "";
});

afterEach(() => {
  return "";
});

beforeAll(() => {
  return "";
});

afterAll(() => {
  return "";
});


// check if interactions work as expected
describe("BDD flow", () => {

    it("given I enter eggs, page loads w three entries a,b,c", () => {
  
    });
  
    
    it("given I click on 'toritilla' recipe entry, new page loads with title and descirption", () => {
  
    });
  });
  

  // check if required input is present
  describe("on the Search Page", () => {
  
    it("check if button present", () => {
      const wrapper = shallow(<SearchIngredientFormView />);
      expect(wrapper.exists('#btn')).toEqual(true);
    
    });
  
    it("check if input present", () => {
      const wrapper = shallow(<SearchIngredientFormView />);
      expect(wrapper.exists('#input')).toEqual(true);
    });
  });
  
  describe("on the Results Page", () => {
  
    it("given there's an entry, check that it is clickable", () => {
      /*
      var mockApiURL =  "/api/recipes?search=ingredients contains ";
      Object.defineProperty(window.location, 'href', {
        writable: true,
        value: 'eggs'
      });

      const wrapper = mount(<RecipesResultsListView location = {window.location.href} />);
      wrapper.instance().LINK_SEARCHPAGE = '/results?searchTermIngredient=';
      
      wrapper.instance().triggeIngredientsSearchQuery();
      console.log (wrapper.debug())
      expect(wrapper.exists('entry')).toEqual(true);
    */ 
   // above does not work due to async nature of API call - requires sth like nightwatch
    });
  });
  