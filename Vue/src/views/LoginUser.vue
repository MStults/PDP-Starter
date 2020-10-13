<template>
  <div>
    <form @submit.prevent="login">
      <label for="username">
        Username:
      </label>
      <input v-model="username" type="text" name="username" value />

      <label for="password">
        Password:
      </label>
      <input v-model="password" type="password" name="password" value />

      <button type="submit" name="button">
        Login
      </button>
      <router-link to="/register">
        Don't have an account? Register.
      </router-link>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: '',
      password: ''
    }
  },
  methods: {
    login() {
      this.$store
        .dispatch('login', {
          username: this.username,
          password: this.password
        })
        .then(() => {
          this.$router.push({ name: 'dashboard' })
        })
        .catch(err => {
          debugger
          console.log(err.response)
        })
    }
  }
}
</script>

<style lang="scss" scoped>
</style>