import React from 'react';
import { StyleSheet, Text, View } from 'react-native'
import { Menu } from '../sections/menu'

// import styles from './styles';

export class Main extends React.Component {
    static navigationOptions = {
        header: null
    }

    render() {
        const { navigate } = this.props.navigation
        
        return(
            <View style={styles.container}>
                <View style={styles.body}>
                    <Text>Patcha Wallet</Text>
                </View>               
                <Menu navigate={navigate} />
            </View>
        );
    }
    
}

export default Main;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    },
    body: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    }
})