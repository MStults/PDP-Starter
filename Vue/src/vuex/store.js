import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: { token: '' },
  mutations: {
    SET_TOKEN(state, token) {
      state.token = token
      localStorage.setItem('token', token)
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_TEST(state, val) {
      state.test = val
    },
    LOGOUT() {
      localStorage.removeItem('token')
      location.reload()
    }
  },
  actions: {
    register({ commit }, credentials) {
      return axios
        .post('//localhost:5000/auth/register', credentials)
        .then(({ data }) => {
          commit('SET_TOKEN', data.data)
        })
    },
    login({ commit }, credentials) {
      return axios
        .post('//localhost:5000/auth/login', credentials)
        .then(({ data }) => {
          commit('SET_TOKEN', data.data)
        })
    },
    logout({ commit }) {
      commit('LOGOUT')
    }
  },
  getters: {
    loggedIn: state => {
      return !!state.token
    },
    token: state => {
      return state.token
    }
  }
})
