import React, {Button} from "react";
import ReactDOM from "react-dom";
import { MemoryRouter } from "react-router-dom";
import App from "./App";
import { shallow } from "enzyme";

import axios from 'axios';

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
    shallow(<App />);
  });


  // comparation to SNAPSHOTS??
  it("check if render contains an element", () => {
    const wrapper = shallow(<App />);
    const welcome = <h2>Welcome to React</h2>;
    expect(wrapper.contains(welcome)).toEqual(true);
  });
});

test.only("running a test in isolation", () => {
  expect(true).toBe(true);
});


it("use a mock function to see if a function was called", () => {
// use cases: callbacks
  const mockOnPress = jest.fn();
  var instance = shallow(
    <Button
      label="test label"
      onPress={mockOnPress}
      url="https://www.test.com"
    />
  ).instance();
  jest.clearAllMocks();

  instance.onPressHandler();
  expect(mockOnPress).toHaveBeenCalled();
  expect(mockOnPress).toHaveBeenCalledTimes(1);
});


