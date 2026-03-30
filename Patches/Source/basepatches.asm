lorom

org $889D2B : JMP SetLives

org $889D8C : JSR GetSong

; rewrite to reverse parity of the check
org $88C9C9
	LDA.w #$0005
	LDX.w $1B85
	BNE ++
	JSR GetSong
++


org $88F800
SetLives:
	LDA.l StartingLives
	STA.w $1B8C
	RTS

GetSong:
	LDA.l RoundGameSong
	RTS

;===================================================================================================

org $808EAC
	JSL OnSRAMInit


org $8187F6
	JSL OnRoundSelected
	BRA ++ : ++

;===================================================================================================
;===================================================================================================
;===================================================================================================

org $81EA00
OnSRAMInit:
	JSL $81AC7E ; vanilla code

	SEP #$20

	LDA.l InitialButtonSelect
	AND.b #$01
	STA.l $7000AE

	REP #$20

	LDA.l InitialRNG
	STA.l SeedSRAM

	RTL

;===================================================================================================

OnRoundSelected:
	; Seed RNGs
	LDA.b TICK
	BNE .fine

	LDA.l InitialRNG

.fine
	STA.w SeedWRAM+0

	LDA.l RoundReseed
	STA.w SeedWRAM+2

	LDA.w #$002F
	STA.w $0802

	RTL

;===================================================================================================
