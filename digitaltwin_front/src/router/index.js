import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    //component: () => import('../views/HomeView.vue'),
    redirect: '/digitalAnalytics',
  },
  {
    path: '/digitalAnalytics',
    name: 'digitalAnalytics',
    component: () => import('../views/digitalAnalytics.vue')
  },
  {
    path: '/waysSelect',
    name: 'waysSelect',
    component: () => import('../views/waysSelect.vue')
  },
  {
    path: '/digitalTwin',
    name: 'digitalTwin',
    component: () => import('../views/digitalTwin.vue')
  },
  {
    path: '/dataTest',
    name: 'dataTest',
    component: () => import('../views/dataTest.vue')
  },
  {
    path: '/statisticalForm',
    name: 'statisticalForm',
    component: () => import('../views/statisticalForm.vue')
  },
  {
    path: '/dataRecord',
    name: 'dataRecord',
    component: () => import('../views/dataRecord.vue')
  },
  {
    path: '/alarmStatus',
    name: 'alarmStatus',
    component: () => import('../views/alarmStatus.vue')
  },
  {
    path: '/deviceRecord',
    name: 'deviceRecord',
    component: () => import('../views/deviceRecord.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
