<template>
    <v-container fill-height fluid grid-list-xl>
        <v-layout wrap>
            <v-flex md12 sm12 lg4>
                <material-chart-card :data="dailyCourseRegistration.data" :options="dailyCourseRegistration.options" color="info" type="Line">
                    <h1>Hi {{user.unique_name}}!</h1>
                    <h4 class="title font-weight-light">Daily Course Registration</h4>
                    <p class="category d-inline-flex font-weight-light">
                        <v-icon color="green" small>mdi-arrow-up</v-icon>
                        <span class="green--text"> {{dailyCourseRegistrationPercentIncrease()}}%</span>&nbsp;
                        increase in today's sales
                    </p>

                    <template slot="actions">
                        <v-icon class="mr-2" small>mdi-clock-outline</v-icon>
                        <span class="caption grey--text font-weight-light">updated 4 minutes ago</span>
                    </template>
                </material-chart-card>
            </v-flex>
            <v-flex md12 sm12 lg4>
                <material-chart-card :data="candidatesActiveChart.data" :options="candidatesActiveChart.options"
                                     :responsive-options="candidatesActiveChart.responsiveOptions" color="red" type="Bar">
                    <h4 class="title font-weight-light">Candidates Active</h4>
<!--                    <p class="category d-inline-flex font-weight-light">Last Campaign Performance</p>-->

                    <template slot="actions">
                        <v-icon class="mr-2" small>mdi-clock-outline</v-icon>
                        <span class="caption grey--text font-weight-light">updated 10 minutes ago</span>
                    </template>
                </material-chart-card>
            </v-flex>
            <v-flex md12 sm12 lg4>
                <material-chart-card :data="dataCompletedTasksChart.data" :options="dataCompletedTasksChart.options"
                                     color="green" type="Line">
                    <h3 class="title font-weight-light">Completed Tasks</h3>
                    <p class="category d-inline-flex font-weight-light">Last Last Campaign Performance</p>

                    <template slot="actions">
                        <v-icon class="mr-2" small>mdi-clock-outline</v-icon>
                        <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
                    </template>
                </material-chart-card>
            </v-flex>
            <v-flex sm6 xs12 md6 lg3>
                <material-stats-card color="green" icon="mdi-store" title="Revenue" value="$34,245"
                                     sub-icon="mdi-calendar" sub-text="Last 24 Hours">
                </material-stats-card>
            </v-flex>
            <v-flex sm6 xs12 md6 lg3>
                <material-stats-card color="orange" icon="mdi-content-copy" title="Used Space" value="49/50"
                                     small-value="GB" sub-icon="mdi-alert" sub-icon-color="error" sub-text="Get More Space..."
                                     sub-text-color="text-primary">
                </material-stats-card>
            </v-flex>
            <v-flex sm6 xs12 md6 lg3>
                <material-stats-card color="red" icon="mdi-information-outline" title="Fixed Issues" value="75"
                                     sub-icon="mdi-tag" sub-text="Tracked from Github">
                </material-stats-card>
            </v-flex>
            <v-flex sm6 xs12 md6 lg3>
                <material-stats-card color="info" icon="mdi-twitter" title="Followers" value="+245"
                                     sub-icon="mdi-update" sub-text="Just Updated">
                </material-stats-card>
            </v-flex>
            <v-flex md12 lg6>
                <material-card color="teal" title="Instructor Assignments" text="Instructor Assignments. Last Updated">
                    <v-data-table :headers="headers" :items="items" hide-actions>
                        <template slot="headerCell" slot-scope="{ header }">
                            <span class="font-weight-light text-warning text--darken-3" v-text="header.text"></span>
                        </template>
                        <template slot="items" slot-scope="{ index, item }">
                            <td>{{ index + 1 }}</td>
                            <td>{{ item.name }}</td>
                            <td>{{ item.course }}</td>
                            <td>{{ item.city }}</td>
                        </template>
                    </v-data-table>
                </material-card>
            </v-flex>
            <v-flex md12 lg6>
                <material-card class="card-tabs" color="green">
                    <v-flex slot="header">
                        <v-tabs v-model="tabs" color="transparent" slider-color="white">
                            <span class="subheading font-weight-light mr-3" style="align-self: center">Tasks:</span>
                            <v-tab class="mr-3">
                                <v-icon class="mr-2">mdi-bug</v-icon>
                                Bugs
                            </v-tab>
                            <v-tab class="mr-3">
                                <v-icon class="mr-2">mdi-code-tags</v-icon>
                                Website
                            </v-tab>
                            <v-tab>
                                <v-icon class="mr-2">mdi-cloud</v-icon>
                                Server
                            </v-tab>
                        </v-tabs>
                    </v-flex>

                    <v-tabs-items v-model="tabs">
                        <v-tab-item v-for="n in 3" :key="n">
                            <v-list three-line>
                                <v-list-tile @click="complete(0)">
                                    <v-list-tile-action>
                                        <v-checkbox :value="list[0]" color="green"/>
                                    </v-list-tile-action>
                                    <v-list-tile-title>
                                        Sign contract for "What are conference organized afraid of?"
                                    </v-list-tile-title>
                                    <div class="d-flex">
                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="success" icon>
                                                <v-icon color="primary">mdi-pencil</v-icon>
                                            </v-btn>
                                            <span>Edit</span>
                                        </v-tooltip>
                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="danger" icon>
                                                <v-icon color="error">mdi-close</v-icon>
                                            </v-btn>
                                            <span>Close</span>
                                        </v-tooltip>
                                    </div>
                                </v-list-tile>
                                <v-divider/>
                                <v-list-tile @click="complete(1)">
                                    <v-list-tile-action>
                                        <v-checkbox :value="list[1]" color="success"/>
                                    </v-list-tile-action>
                                    <v-list-tile-title>
                                        Lines From Great Russian Literature? Or E-mails From My Boss?
                                    </v-list-tile-title>
                                    <div class="d-flex">
                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="success" icon>
                                                <v-icon color="primary">mdi-pencil</v-icon>
                                            </v-btn>
                                            <span>Edit</span>
                                        </v-tooltip>

                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="danger" icon>
                                                <v-icon color="error">mdi-close</v-icon>
                                            </v-btn>
                                            <span>Close</span>
                                        </v-tooltip>
                                    </div>
                                </v-list-tile>
                                <v-divider/>
                                <v-list-tile @click="complete(2)">
                                    <v-list-tile-action>
                                        <v-checkbox :value="list[2]" color="success"/>
                                    </v-list-tile-action>
                                    <v-list-tile-title>
                                        Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit
                                    </v-list-tile-title>
                                    <div class="d-flex">
                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="success" icon>
                                                <v-icon color="primary">mdi-pencil</v-icon>
                                            </v-btn>
                                            <span>Edit</span>
                                        </v-tooltip>
                                        <v-tooltip top content-class="top">
                                            <v-btn slot="activator" class="v-btn--simple" color="danger" icon>
                                                <v-icon color="error">mdi-close</v-icon>
                                            </v-btn>
                                            <span>Close</span>
                                        </v-tooltip>
                                    </div>
                                </v-list-tile>
                            </v-list>
                        </v-tab-item>
                    </v-tabs-items>
                </material-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
    export default {
        computed: {
            user() {
                return this.$jwt.decode(this.$store.state.authentication.token);
            },

        },
        data() {
            return {
                dailyCourseRegistration: {
                    data: {
                        labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
                        series: [
                            [12, 17, 7, 17, 23, 18, 38],
                        ],
                    },
                    options: {
                        lineSmooth: this.$chartist.Interpolation.cardinal({
                            tension: 0,
                        }),
                        low: 0,
                        high: 50, // creative tim: we recommend you to set the high sa the biggest value
                        // + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0,
                        },
                    },
                },
                candidatesActiveChart: {
                    data: {
                        labels: ['Ja', 'Fe', 'Ma', 'Ap', 'Mai', 'Ju', 'Jul', 'Au', 'Se', 'Oc', 'No', 'De'],
                        series: [
                            [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895],
                        ],
                    },
                    options: {
                        axisX: {
                            showGrid: false,
                        },
                        low: 0,
                        high: 1000,
                        chartPadding: {
                            top: 0,
                            right: 5,
                            bottom: 0,
                            left: 0,
                        },
                    },
                    responsiveOptions: [
                        ['screen and (max-width: 640px)', {
                            seriesBarDistance: 5,
                            axisX: {
                                labelInterpolationFnc: (value) => value[0],
                            },
                        }],
                    ],
                },
                dataCompletedTasksChart: {
                    data: {
                        labels: ['12am', '3pm', '6pm', '9pm', '12pm', '3am', '6am', '9am'],
                        series: [
                            [230, 750, 450, 300, 280, 240, 200, 190],
                        ],
                    },
                    options: {
                        lineSmooth: this.$chartist.Interpolation.cardinal({
                            tension: 0,
                        }),
                        low: 0,
                        high: 1000, // creative tim: we recommend you to set the high sa the biggest value
                        // + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0,
                        },
                    },
                },
                headers: [
                    {
                        sortable: true,
                        text: 'ID',
                        value: 'id',
                    },
                    {
                        sortable: true,
                        text: 'Name',
                        value: 'name',
                    },
                    {
                        sortable: true,
                        text: 'Course',
                        value: 'course',
                    },
                    {
                        sortable: true,
                        text: 'City',
                        value: 'city',
                    },
                ],
                items: [
                    {
                        name: 'Dakota Rice',
                        city: 'Cardiff',
                        course: 'Flight School',
                    },
                    {
                        name: 'Minerva Hooper',
                        city: 'Somerset',
                        course: 'Ground School',
                    }, {
                        name: 'Sage Rodriguez',
                        city: 'Dorset',
                        course: 'Flight Assessment',
                    }, {
                        name: 'Philip Chanley',
                        city: 'Aberdeen',
                        course: 'Ground School',
                    }, {
                        name: 'Doris Greene',
                        city: 'Cardiff',
                        course: 'Operators Manual',
                    },
                ],
                tabs: 0,
                list: {
                    0: false,
                    1: false,
                    2: false,
                },
            };
        },
        methods: {
            complete(index) {
                this.list[index] = !this.list[index];
            },
            dailyCourseRegistrationPercentIncrease() {
                return Math.round(100.00 - (this.dailyCourseRegistration.data.series[0][5]
                    / this.dailyCourseRegistration.data.series[0][6]) * 100.00);
            },
        },
    };
</script>
