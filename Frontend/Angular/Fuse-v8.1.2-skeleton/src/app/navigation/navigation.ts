import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'dashboards',
        title    : 'Dashboards',
        translate: 'NAV.DASHBOARDS',
        type     : 'group',
        children : [
            {
                id: 'analytics',
                title: 'Analytics',
                translate: 'NAV.ANALYTICS.TITLE',
                type: 'item',
                icon: 'dashboard',
                url: '/apps/analytics'
            }
        ]
    },
    {
        id: 'forms',
        title: 'Forms',
        translate: 'NAV.FORMS',
        type: 'group',
        children: [
            {
                id: 'stocktransaction',
                title: 'Stock Transaction',
                translate: 'NAV.STOCKTRANSACTION.TITLE',
                type: 'item',
                icon: 'attach_money',
                url: '/forms/stocktransaction'
            }
        ]
    }
];
