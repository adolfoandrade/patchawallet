import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import { AnalyticsComponent } from './analytics.component';

const routes: Routes = [
    {
        path: '**',
        component: AnalyticsComponent
    }
];

@NgModule({
    declarations: [
        AnalyticsComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        FuseSharedModule
    ],
})
export class AnalyticsComponentModule {
}

