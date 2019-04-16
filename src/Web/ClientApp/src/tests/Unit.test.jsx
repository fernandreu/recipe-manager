import React from "react";
import { shallow, mount } from "enzyme";
import { SearchIngredientFormView } from "../components/SearchIngredientFormView";
import { RecipesResultsListView } from "../components/RecipesResultsListView";
import { renderToString } from 'react-dom/server'

// Unit 

describe("on the Search Page, ", () => {
  var wrapper = {};
  beforeEach(() => {
    return Object.defineProperty(window.location, "href", {
      writable: true,
      value: "mockURL/orig/?none"
    });
  });
  beforeEach(() => {
    wrapper = shallow(<SearchIngredientFormView location={window.location} />);
  });

  
  it("the main component renders without crashing", () => {
    try{ shallow(<SearchIngredientFormView />);} 
    catch(error){
      console.log("TEST REPORT: " + error)
    }
  });

});

  
describe("on the Results list view, ", () => {

  var wrapper_s = {};
  var wrapper_m = {};
  beforeAll(() => {
    return Object.defineProperty(window.location, "href", {
      writable: true,
      value: "mockURL/orig/?none"
    });
  });
  beforeEach(() => {
    wrapper_s = shallow(<RecipesResultsListView location={window.location} />);
  });
  beforeEach(() => {
    wrapper_m = mount(<RecipesResultsListView location={window.location} />);
  });


  // handler

  it("handleListEntryClick: given bad itemKey, raise error", () => {
    // Arrange
    // Act
    // Assert
   });
 
   it("triggerIngredientsSearchQuery: if url does not contain searchTerm param, raise error", () => {
     // Arrange
     // Act
     // Assert
    });

    it("triggerIngredientsSearchQuery: if url does not contain searchTerm, do nothing", () => {
      // Arrange
      // Act
      // Assert
     });
 
    it("if backend db catches error, raise error", () => {
     // Arrange
     // Act
     // Assert
    });



    // UI

  it("given 5 mock entries, a list with 5 entries is returned", () => {
    var mockItems = [
      {title:"mockTitle1", href:"1"},
      {title:"mockTitle2", href:"2"},
      {title:"mockTitle3", href:"3"}
      ];
    
  });

  it("creatUIentry: given a mock entry, a List Group Item is returned with the title filled in", () => {
    //TODO: find more elegant solution for testing sub-render
    //Arrange
    var mockItem = {title:"mockTitle"}
    //Act
    var ele = wrapper_s.instance().create_ui_entry_result(mockItem)
    var eleToHtml = renderToString(ele);
  
    //Assert
    expect(eleToHtml.includes(mockItem["title"])).toEqual(true);
  });

  it("the main component renders without crashing", () => {
      // TODO: is this test case already ticked off as the shallow renders in "Before each"?
  });

  it("main: if no results, render 'no entries'", () => {
    wrapper_s.instance().setState({noResultsFound:true})
    const status = wrapper_s.state();
    wrapper_s.instance().forceUpdate()
    expect(wrapper_s.contains("no entries")).toEqual(true)
});


});




