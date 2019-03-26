<template>
    <div>
        <input type="checkbox" id="sidebarToggler" name="sidebarToggler" />
        <!--<input type="button" id="usertoggler" name="usertoggler" />-->
        <div class="wrapper">
            <label for="sidebarToggler" class="nav-toggle">☰</label>
            <!--<label for="usertoggler" class="user-toggle">☰</label>-->

            <aside class="sidebar">
                <nav class="main-nav">
                    <ul class="nav">
                        <li>Home</li>
                        <li>Sounds</li>
                        <li>Shows</li>
                        <li>Bio</li>
                        <li>Gallery</li>
                    </ul>
                </nav>
            </aside>
            <p> Hello World! </p>
        </div>
    </div>
</template>

<style lang="scss">
    @import '../sass/_bourbon-neat.scss';
    @import '../sass/2-base/_base.scss';
</style>

<style>
button,
input[type="submit"] {
  -webkit-font-smoothing: antialiased;
  background-color: #477DCA;
  border-radius: 3px;
  color: white;
  display: inline-block;
  font-size: 1em;
  font-weight: bold;
  line-height: 1;
  padding: 0.75em 1em;
  text-decoration: none; }
  button:hover,
  input[type="submit"]:hover {
    background-color: #2c5999;
    color: white; }
  button:disabled,
  input[type="submit"]:disabled {
    cursor: not-allowed;
    opacity: 0.5; }

body {
  -webkit-font-smoothing: antialiased;
  background-color: white;
  color: #333;
  font-family: "Helvetica Neue", Helvetica, Roboto, Arial, sans-serif;
  font-size: 1em;
  line-height: 1.5; }

h1,
h2,
h3,
h4,
h5,
h6 {
  font-family: "Helvetica Neue", Helvetica, Roboto, Arial, sans-serif;
  line-height: 1.25;
  margin: 0;
  text-rendering: optimizeLegibility; }

h1 {
  font-size: 2.25em; }

h2 {
  font-size: 2em; }

h3 {
  font-size: 1.75em; }

h4 {
  font-size: 1.5em; }

h5 {
  font-size: 1.25em; }

h6 {
  font-size: 1em; }

p {
  margin: 0 0 0.75em; }

a {
  -webkit-transition: color 0.1s linear;
  -moz-transition: color 0.1s linear;
  transition: color 0.1s linear;
  color: #477DCA;
  text-decoration: none; }
  a:hover {
    color: #2c5999; }
  a:active, a:focus {
    color: #2c5999;
    outline: none; }

hr {
  border-bottom: 1px solid #DDD;
  border-left: none;
  border-right: none;
  border-top: none;
  margin: 1.5em 0; }

img,
picture {
  margin: 0;
  max-width: 100%; }

blockquote {
  border-left: 2px solid #DDD;
  color: #595959;
  margin: 1.5em 0;
  padding-left: 0.75em; }

cite {
  color: #737373;
  font-style: italic; }
  cite:before {
    content: "\2014 \00A0"; }

fieldset {
  background: #f7f7f7;
  border: 1px solid #DDD;
  margin: 0 0 0.75em 0;
  padding: 1.5em; }

input,
label,
select {
  display: block;
  font-family: "Helvetica Neue", Helvetica, Roboto, Arial, sans-serif;
  font-size: 1em; }

label {
  font-weight: bold;
  margin-bottom: 0.375em; }
  label.required:after {
    content: "*"; }
  label abbr {
    display: none; }

textarea,
input[type="email"], input[type="number"], input[type="password"], input[type="search"], input[type="tel"], input[type="text"], input[type="url"], input[type="color"], input[type="date"], input[type="datetime"], input[type="datetime-local"], input[type="month"], input[type="time"], input[type="week"],
select[multiple=multiple] {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
  -webkit-transition: border-color;
  -moz-transition: border-color;
  transition: border-color;
  background-color: white;
  border-radius: 3px;
  border: 1px solid #DDD;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06);
  font-family: "Helvetica Neue", Helvetica, Roboto, Arial, sans-serif;
  font-size: 1em;
  margin-bottom: 0.75em;
  padding: 0.5em 0.5em;
  width: 100%; }
  textarea:hover,
  input[type="email"]:hover, input[type="number"]:hover, input[type="password"]:hover, input[type="search"]:hover, input[type="tel"]:hover, input[type="text"]:hover, input[type="url"]:hover, input[type="color"]:hover, input[type="date"]:hover, input[type="datetime"]:hover, input[type="datetime-local"]:hover, input[type="month"]:hover, input[type="time"]:hover, input[type="week"]:hover,
  select[multiple=multiple]:hover {
    border-color: #c4c4c4; }
  textarea:focus,
  input[type="email"]:focus, input[type="number"]:focus, input[type="password"]:focus, input[type="search"]:focus, input[type="tel"]:focus, input[type="text"]:focus, input[type="url"]:focus, input[type="color"]:focus, input[type="date"]:focus, input[type="datetime"]:focus, input[type="datetime-local"]:focus, input[type="month"]:focus, input[type="time"]:focus, input[type="week"]:focus,
  select[multiple=multiple]:focus {
    border-color: #477DCA;
    box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.06), 0 0 5px rgba(55, 112, 192, 0.7);
    outline: none; }

textarea {
  resize: vertical; }

input[type="search"] {
  -webkit-appearance: none;
  -moz-appearance: none;
  -ms-appearance: none;
  -o-appearance: none;
  appearance: none; }

input[type="checkbox"],
input[type="radio"] {
  display: inline;
  margin-right: 0.375em; }

input[type="file"] {
  padding-bottom: 0.75em;
  width: 100%; }

select {
  margin-bottom: 1.5em;
  max-width: 100%;
  width: auto; }

table {
  border-collapse: collapse;
  margin: 0.75em 0;
  table-layout: fixed;
  width: 100%; }

th {
  border-bottom: 1px solid #b7b7b7;
  font-weight: bold;
  padding: 0.75em 0;
  text-align: left; }

td {
  border-bottom: 1px solid #DDD;
  padding: 0.75em 0; }

tr,
td,
th {
  vertical-align: middle; }

ul,
ol {
  margin: 0;
  padding: 0;
  list-style-type: none; }
dl {
  margin-bottom: 0.75em; }
  dl dt {
    font-weight: bold;
    margin-top: 0.75em; }
  dl dd {
    margin: 0; }

button,
input[type="submit"] {
  -webkit-appearance: none;
  -moz-appearance: none;
  -ms-appearance: none;
  -o-appearance: none;
  appearance: none;
  border: none;
  cursor: pointer;
  user-select: none;
  vertical-align: middle;
  white-space: nowrap; }

/* Neat 1.7.0
 * http://neat.bourbon.io
 * Copyright 2012-2014 thoughtbot, inc.
 * MIT License */
html {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box; }

*, *:before, *:after {
  -webkit-box-sizing: inherit;
  -moz-box-sizing: inherit;
  box-sizing: inherit; }

body {
  font-family: "Helvetica Neue", Helvetica, Roboto, Arial, sans-serif; }

body {
  padding: 0;
  margin: 0; }

.page-content {
  position: relative;
  z-index: 0; }

.nav-toggle {
  width: 50px;
  height: 50px;
  text-decoration: none;
  font-size: 30px;
  color: #d3d3d3;
  -webkit-transition: all 0.15s ease-out 0s;
  -moz-transition: all 0.15s ease-out 0s;
  transition: all 0.15s ease-out 0s;
  position: fixed;
  top: 20px;
  right: 0;
  bottom: 0;
  left: 30px;
  z-index: 1; }

.user-toggle {
    width: 50px;
    height: 50px;
    text-decoration: none;
    font-size: 30px;
    color: #d3d3d3;
    -webkit-transition: all 0.15s ease-out 0s;
    -moz-transition: all 0.15s ease-out 0s;
    transition: all 0.15s ease-out 0s;
    position: fixed;
    top: 20px;
    right: 30px;
    bottom: 0;
    z-index: 1; }

.sidebar {
  -webkit-transition: all 0.15s ease-out 0s;
  -moz-transition: all 0.15s ease-out 0s;
  transition: all 0.15s ease-out 0s;
  position: fixed;
  top: 0px;
  right: 0;
  bottom: 0px;
  left: -230px;
  width: 150px;
  padding: 30px;
  background: rgba(51, 51, 51, 0.9);
  z-index: 1; }
  .sidebar li {
    color: rgba(255, 255, 255, 0.8);
    font-size: 16px;
    margin-bottom: 16px;
    -webkit-font-smoothing: antialiased;
    cursor: pointer;
    text-align: center; }
    .sidebar li:hover {
      color: white; }

#sidebarToggler {
  display: none; }
  #sidebarToggler:checked + .wrapper .sidebar {
    left: 0px; }
  #sidebarToggler:checked + .wrapper .nav-toggle {
    left: 200px; }

#userToggler {
  display: none; }
  #userToggler:checked + .wrapper .sidebar {
    right: 0px; }
  #userToggler:checked + .wrapper .nav-toggle {
    right: 200px; }


</style>
