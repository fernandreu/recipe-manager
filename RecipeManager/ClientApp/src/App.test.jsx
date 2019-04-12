import React from "react";
import Button from "react";
import ReactDOM from "react-dom";
import { MemoryRouter } from "react-router-dom";
import App from "./App";
import { shallow, mount } from "enzyme";
import TestRenderer from 'react-test-renderer';
import axios from 'axios';
import { SearchIngredientFormView } from "./components/SearchIngredientFormView";

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

describe("test the following group", () => {
  beforeEach(() => {
    // only for this group
    return "";
  });

  it("check if comp renders without crashing", () => {
    // Arrange
    // Act
    // Assert
    shallow(<App />);
  });

  // comparation to SNAPSHOTS??
  it("check if render contains an element", () => {
    const wrapper = shallow(<App />);
    const welcome = <h2>Welcome to React</h2>;
    expect(wrapper.contains(welcome)).toEqual(true);
  });
});

/*
test.only("running a test in isolation", () => {
  expect(true).toBe(true);
});
*/

it("use a mock function to see if a function was called", () => {
// use cases: callbacks
  /*const mockOnPress = jest.fn();
  const wrapper = shallow(<SearchIngredientFormView/>)
  jest.clearAllMocks();

  wrapper.find('#btn').simulate('click');
  expect(mockOnPress).toHaveBeenCalled();
  expect(mockOnPress).toHaveBeenCalledTimes(1);*/
});


// BDD
describe("test if search page contains required interaction elements", () => {

  it("check if button present", () => {
    const wrapper = shallow(<SearchIngredientFormView />);
    expect(wrapper.exists('#btn')).toEqual(true);
  
  });

  it("check if input present", () => {
    const wrapper = shallow(<SearchIngredientFormView />);
    expect(wrapper.exists('#input')).toEqual(true);
  });
});

// Integration

describe("test the search page", () => {

  it("triggeringIngredientesSearch ", () => {
    Object.defineProperty(window.location, 'href', {
      writable: true,
      value: 'mockURL/orig/'
    });


    const wrapper = shallow(<SearchIngredientFormView />);
    wrapper.instance().LINK_SEARCHPAGE = "mockUrl/changed/"
    wrapper.instance().triggerIngredientSearch("mockTerm")
    console.log(window.location.href)
  });


});