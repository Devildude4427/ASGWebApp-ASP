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
            .then((response) => {
                if (response.data != null) {
                    this.user = response.data;
                } else {
                    this.user = { lastCompletedStage : -1};
                }
            });
    }
});