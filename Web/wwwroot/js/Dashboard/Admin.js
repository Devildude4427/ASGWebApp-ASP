new Vue({
    el: '#adminDashboard',
    data () {
        return {
            candidateCount: 0
        }
    },
    mounted () {
        axios
            .get('/api/v1/statistics/candidateCount')
            .then((response) => {
                if (response.data != null) {
                    this.candidateCount = response.data;
                } else {
                    this.candidateCount = 0;
                }
            });
    }
});