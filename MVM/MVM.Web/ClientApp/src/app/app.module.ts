import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { ListCorrespondenceComponent } from './list-correspondence/list-correspondence.component';
import { ConstantsService } from 'src/shared/constants.service';
import { AuthInterceptorService } from 'src/shared/services/_base-http.service';
import { CorrespondenceService } from 'src/shared/services/correspondence.service';
import { AddCorrespondenceComponent } from './add-correspondence/add-correspondence.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ListCorrespondenceComponent,
    AddCorrespondenceComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'list-correspondence', component: ListCorrespondenceComponent },
      { path: 'add-correspondence', component: AddCorrespondenceComponent },
      { path: 'add-correspondence/:Id', component: AddCorrespondenceComponent }
    ])
  ],
  providers: [
    ConstantsService,
    CorrespondenceService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
