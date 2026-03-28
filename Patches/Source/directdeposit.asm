org $898C97
DirectDeposit:
	LDA.w $1ABC
	BEQ .exit

.remove_50
	CMP.b #50
	BCC .add_remaining

	INC.w $1B8C

	SBC.b #50
	BRA .remove_50

.add_remaining
	ADC.w $1B8B

	CMP.b #50
	BCC .not_one_more_life

	INC.w $1B8C

	SBC.b #50

.not_one_more_life
	STA.w $1B8B

	LDA.w $1B8C
	CMP.b #9
	BCC .lives_fine

	LDA.b #9

.lives_fine
	STA.w $1B8C
	STA.w $144F

	STA.l $7F24FC

	STZ.w $1ABA
	STZ.w $1ABC

	JSL $83BCE7

.exit
	RTS

warnpc $898CFF
