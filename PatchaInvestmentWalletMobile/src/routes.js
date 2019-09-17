import React from 'react';
import { createStackNavigator, createAppContainer } from 'react-navigation'

import SignIn from './pages/signIn';
import SignUp from './pages/signUp';
import Main from './pages/main';

const RootStack = createStackNavigator({
    SignIn: {
        screen: SignIn
    },
    Signup: {
        screen: SignUp
    },
    Main: {
        screen: Main
    }
},
    {
        initialRouteName: 'Main'
    });

export default createAppContainer(RootStack);