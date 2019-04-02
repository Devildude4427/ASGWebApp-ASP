import Vue from 'vue';
import axios from 'axios';

const base = axios.create({
  baseURL: process.env.VUE_APP_API_ENDPOINT,
});

Vue.prototype.$http = base;
