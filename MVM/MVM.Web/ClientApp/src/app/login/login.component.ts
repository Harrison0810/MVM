import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageModel } from 'src/shared/models/message.model';
import { ConstantsService } from 'src/shared/constants.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: 'app-login',
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup;

    constructor(
        private _http: HttpClient,
        private _constants: ConstantsService,
        private router: Router,
        private formBuilder: FormBuilder
    ) {
    }

    public Login(): void {
        if (this.loginForm.invalid) {
            return;
        }

        const authModel = {
            'UserName': this.loginForm.controls.username.value,
            'Password': this.loginForm.controls.password.value
        }

        this._http.post(this._constants.URLS.Login(), authModel).subscribe((result: MessageModel<any>) => {
            if (result.status) {
                // Save token in cookie
                // Add http interceptor to add token
                this.SetCookie('MVM_Token', result.data.token);

                // success
                this.router.navigate(['/list-correspondence']);
            } else {
                // error
                console.log(result);
            }
        });
    }

    private SetCookie(name: string, val: string): void {
        const date = new Date();
        const value = val;

        // Set it expire in 1 days
        date.setTime(date.getTime() + (1 * 24 * 60 * 60 * 1000));

        // Set it
        document.cookie = name + '=' + value + '; expires=' + date.toUTCString() + '; path=/';
    }

    private DeleteCookie(name: string): void {
        const date = new Date();

        // Set it expire in -1 days
        date.setTime(date.getTime() + (-1 * 24 * 60 * 60 * 1000));

        // Set it
        document.cookie = name + '=; expires=' + date.toUTCString() + '; path=/';
    }

    ngOnInit(): void {
        this.DeleteCookie('MVM_Token');
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.compose([Validators.required])],
            password: ['', Validators.required]
        });
    }
}