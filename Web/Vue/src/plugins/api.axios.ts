// const base = axios.create({
//   baseURL: process.env.VUE_APP_API_ENDPOINT,
// });
//
// Vue.prototype.$http = base;


import Vue from 'vue';
import axios from 'axios';
import VueAxios from 'vue-axios';
import JwtService from '@/services/jwt-service';
import { API_URL } from '@/configuration/config';

const ApiService = {
  init() {
    Vue.use(VueAxios, axios);
    Vue.axios.defaults.baseURL = API_URL;
  },

  setHeader() {
    Vue.axios.defaults.headers.common.Authorization = `Token ${JwtService.getToken()}`;
  },

  query(resource: any, params: any) {
    return Vue.axios.get(resource, params).catch((error) => {
      throw new Error(`[RWV] ApiService ${error}`);
    });
  },

  get(resource: any, slug = '') {
    return Vue.axios.get(`${resource}/${slug}`).catch((error) => {
      throw new Error(`[RWV] ApiService ${error}`);
    });
  },

  post(resource: any, params: any) {
    return Vue.axios.post(`${resource}`, params);
  },

  update(resource: any, slug: any, params: any) {
    return Vue.axios.put(`${resource}/${slug}`, params);
  },

  put(resource: any, params: any) {
    return Vue.axios.put(`${resource}`, params);
  },

  delete(resource: any) {
    return Vue.axios.delete(resource).catch((error) => {
      throw new Error(`[RWV] ApiService ${error}`);
    });
  },
};

export default ApiService;
