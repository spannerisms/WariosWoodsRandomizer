org $83A3AA : JSL RandomX
org $83A3E9 : JSL RandomX
org $83A9E2 : JSL RandomX
org $83B24E : JSR RandomY
org $83B275 : JSR RandomY

;---------------------------------------------------------------------------------------------------

org $83DA00

RandomX:
	REP #$20

	JSR Random

	SEP #$20

	BIT.b #$80
	BNE .vanilla

	EOR.w $0630

	RTL

.vanilla
	LDA.l $83B138,X

	RTL

;---------------------------------------------------------------------------------------------------

RandomY:
	JSR Random

	BIT.w #$0080
	BNE .vanilla

	EOR.w $0630

	RTS

.vanilla
	LDA.w $83B138,Y

	RTS

;---------------------------------------------------------------------------------------------------

Random:
	LDA.w SeedWRAM+0
	ASL
	ADC.w SeedWRAM+2
	ASL

	ADC.w SeedWRAM+0
	ADC.w #$3211
	STA.w SeedWRAM+2

	EOR.w SeedWRAM+0
	STA.w SeedWRAM+0

	RTS

;===================================================================================================
