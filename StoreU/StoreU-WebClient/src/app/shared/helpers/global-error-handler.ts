import { Injectable, ErrorHandler } from "@angular/core";
import { MatSnackBar } from '@angular/material'; 
import { ActionResponse } from 'src/app/models/action-response.model';

@Injectable()
export class GlobalErrorHandler extends ErrorHandler {
  constructor(private snackBar: MatSnackBar) {
    super();
  }

  handleError(error: any) { 
     this.snackBar.open('An unexpected error has occurred' + error.Message, 'OK', {
      duration: 4000,
    });
  }

  handleFriendlyError(error: ActionResponse) {

    this.snackBar.open(error.friendlyErrorMessage, 'OK', {
      duration: 4000,
    });
  }
}
