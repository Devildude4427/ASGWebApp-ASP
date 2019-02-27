new Vue({
    el: '#app',
    data () {
        return {
            user: 0
        }
    },
    mounted () {
        axios
            .get('/api/v1/user')
            .then(response => (this.user = response.data));
    }
});



