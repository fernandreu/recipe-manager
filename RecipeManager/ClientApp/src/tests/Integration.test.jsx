import React from "react";
import Button from "react";
import ReactDOM from "react-dom";
import { MemoryRouter } from "react-router-dom";
import App from "../App";
import { shallow, mount } from "enzyme";
import TestRenderer from "react-test-renderer";
import axios from "axios";
import { SearchIngredientFormView } from "../components/SearchIngredientFormView";
import { RecipeDetailsView } from "../components/RecipeDetailsView";
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



describe("Group description, ", () => {
  it("Method / Module: Given x, doing y, then z ", () => {
    // Arrange
    // Act
    // Assert
  });
});



// Integration

describe("on the Search List Page, ", () => {
  it("if the ingredients search is triggered, the window href will change to the desired API url and search term ", () => {
    //Arrange
    Object.defineProperty(window.location, "href", {
      writable: true,
      value: "mockURL/orig/"
    });
    var mockUrlChanged = "mockUrl/changed/";
    var mockSearchTerm = "mockSearchTerm";
    const wrapper = shallow(<SearchIngredientFormView />);
    wrapper.instance().LINK_SEARCHPAGE = mockUrlChanged;
    //Act
    wrapper.instance().triggerIngredientSearch(mockSearchTerm);
    //Assert
    expect(window.location.href).toEqual(mockUrlChanged + "" + mockSearchTerm);
  });
});



describe("on the Results Page, ", () => {


  var wrapper = {};

  beforeEach(() => {
    return Object.defineProperty(window.location, "href", {
      writable: true,
      value: "mockURL/orig/?none"
    });
  });

  beforeEach(() => {
    wrapper = shallow(<RecipesResultsListView location={window.location} />);
  });


  it("given a list entry, on handleListrEntryClick, window ref is set correclty", () => {
    //Arrange
    var mockHrefKey =
      "https://localhost:5001/api/recipes/b13ce333-307f-4995-bd88-33d7280d785d";
    var mockKey = "b13ce333-307f-4995-bd88-33d7280d785d";
    var mockURL = "mockURL";
    wrapper.instance().API_URL_RECIPE_EP = mockURL;
    //Act
    wrapper.instance().handleListEntryClick(mockHrefKey);
    //Assert
    expect(window.location.href).toEqual(mockURL + "" + mockKey);
  });


  it("given the URL params, on triggering the ingredient search, the ingredientsquery is triggered correclty ", () => {
    console.log("cannot be tested currently as unsure how to mock backend");
  });


  it("query: given search term 'eggs', the API returns three matching recipes", () => {
    console.log("cannot be tested currently as unsure how to mock backend");
  });


  it("given data, state will be updated accoringley", () => {
    // Arrange
    var mockData = { value: [{ id: 1 }] };
    wrapper.instance().setState ( {
      resultsingredientSearch: []
    });
    //Act
    wrapper.instance().updateIngredientsSearchResults(mockData);
    const status = wrapper.state(); // INFO: use .state() to get state AFTER update, as setState is async!
    // Assert
    expect(status.resultsingredientSearch).toEqual(mockData.value);
  });

});



describe("on the Recipe View Page, ", () => {
  it("given entry URL params with recipe key '[provide example input]', backend returns recipe '[provide example result]' ", () => {
    console.log("cannot be tested currently as unsure how to mock backend");
  });
});
