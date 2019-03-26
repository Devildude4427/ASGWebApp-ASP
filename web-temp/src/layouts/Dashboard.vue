<script>
import NavBar from '@/components/_NavBar';
// import SideBar from '@/components/_SideBar';
import SideBar from '@/components/_SideBar2';
import Footer from '@/components/_Footer';

export default {
    components: {
        NavBar,
        SideBar,
        Footer,
    },
};
</script>

<template>
    <div>
        <div id="main-wrapper">
            <nav-bar />

            <side-bar />

            <slot />

            <p>Testing Testing 123</p>
        </div>
    </div>
</template>