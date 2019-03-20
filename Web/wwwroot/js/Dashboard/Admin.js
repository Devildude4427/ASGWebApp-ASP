new Vue({
    el: '#adminDashboard',
    data () {
        return {
            candidateCount: 0,
            newCandidateCount: 0
        }
    },
    mounted () {
        axios
            .get('/api/v1/statistics/candidateCount')
            .then((response) => {
                if (response.data != null) {
                    this.candidateCount = response.data;
                }
            });
        axios
            .get('/api/v1/statistics/newCandidateCount')
            .then((response) => {
                if (response.data != null) {
                    this.newCandidateCount = response.data;
                }
            });
    }
});