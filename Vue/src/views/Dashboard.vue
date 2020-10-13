<template>
  <div>
    <h1>Dashboard</h1>
    <!-- <h2>{{ this.$store.state.user }}</h2> -->
    <h2>{{ loggedIn }}</h2>
    <template v-if="!isLoading">
      <EventCard v-for="event in events" :key="event.id" :event="event" />
    </template>
    <p v-else>
      Loading events
    </p>
  </div>
</template>

<script>
import axios from 'axios'
import EventCard from '../components/EventCard'
import { authComputed } from '../vuex/helper.js'

export default {
  components: { EventCard },
  data() {
    return {
      isLoading: true,
      events: []
    }
  },
  created() {
    axios.get('//localhost:5000/test/dashboard').then(({ data }) => {
      this.events = data
      this.isLoading = false
    })
  },
  computed: {
    ...authComputed
  }
}
</script>
